using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class Organizer
    {
        [Key]
        public int OrganizerId { get; set; }

        [Required(ErrorMessage = "Podaj imię organizatora")]
        [MaxLength(64, ErrorMessage = "Imię nie może przekraczać 64 znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko organizatora")]
        [MaxLength(64, ErrorMessage = "Nazwisko nie może przekraczać 64 znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Podaj datę urodzenia")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "Podaj hasło")]
        [MaxLength(256, ErrorMessage = "Hasło nie może przekraczać 256 znaków")]
        public string Password { get; set; }
    }
}
