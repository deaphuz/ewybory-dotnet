using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }

        //typ wyborów - prezydenckie parlamentarne etc etc
        [Required(ErrorMessage = "Nie podano typu wyborów")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko kandydata")]
        [MaxLength(64, ErrorMessage = "Nazwisko nie może przekraczać 64 znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Nie podano daty rozpoczęcia wyborów")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "Nie podano daty zakończenia wyborów")]
        public DateTime EndDate { get; set; }

        public int MaxVotesPerVoter { get; set; }

        //frekwencja wyborcza
        public float VoterTournout { get; set; }
    }
}
