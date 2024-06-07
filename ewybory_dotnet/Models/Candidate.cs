using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Podaj imię kandydata")]
        [MaxLength(64, ErrorMessage = "Imię nie może przekraczać 64 znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko kandydata")]
        [MaxLength(64, ErrorMessage = "Nazwisko nie może przekraczać 64 znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Podaj datę urodzenia kandydata")]
        public DateTime Birth { get; set; }
    }
}
