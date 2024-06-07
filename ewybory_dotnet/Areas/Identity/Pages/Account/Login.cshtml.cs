using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;

namespace ewybory_dotnet.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ReCaptchaSecret = "6LfNxfIpAAAAAEhopbclrZpabL0hephs1eix6r2F";

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, IHttpClientFactory httpClientFactory)
        {
            _signInManager = signInManager;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var captchaResponse = Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(captchaResponse) || !await IsCaptchaValid(captchaResponse))
            {
                ModelState.AddModelError(string.Empty, "Captcha validation failed.");
                return Page();
            }

            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
        }

        private async Task<bool> IsCaptchaValid(string captchaResponse)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={ReCaptchaSecret}&response={captchaResponse}");

            _logger.LogInformation($"reCAPTCHA response: {response}");

            var captchaVerification = JsonSerializer.Deserialize<ReCaptchaVerificationResponse>(response);

            _logger.LogInformation($"reCAPTCHA verification: {captchaVerification.ToJson()}");
            if (captchaVerification == null)
            {
                _logger.LogError("Failed to deserialize reCAPTCHA response.");
                return false;
            }

            if (!captchaVerification.success)
            {
                _logger.LogError($"reCAPTCHA validation failed");
            }

            return captchaVerification.success;
        }

        private class ReCaptchaVerificationResponse
        {
            public bool success { get; set; }
            public string challenge_ts { get; set; }
        }
    }
}
