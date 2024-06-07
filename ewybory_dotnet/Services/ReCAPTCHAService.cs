namespace ewybory_dotnet.Services
{
    public class ReCAPTCHAService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ReCAPTCHAService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<bool> VerifyToken(string token)
        {
            var secretKey = _configuration["ReCAPTCHA:SecretKey"];
            var response = await _httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                return result.success == "true";
            }
            return false;
        }
    }

}
