using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class Voter
    {
        [Key]
        public int VoterId { get; set; }

        [Required(ErrorMessage = "Podaj PESEL")]
        [StringLength(11, ErrorMessage = "Nieprawidłowa długość numeru PESEL")]
        public string PESEL { get; set; }

        [Required(ErrorMessage = "Podaj imię organizatora")]
        [MaxLength(64, ErrorMessage = "Imię nie może przekraczać 64 znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko organizatora")]
        [MaxLength(64, ErrorMessage = "Nazwisko nie może przekraczać 64 znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Podaj datę urodzenia")]
        public DateTime Birth { get; set; }

    }
}
