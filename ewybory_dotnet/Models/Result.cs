using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewybory_dotnet.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [ForeignKey("Election")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int NumVotes { get; set; }
    }
}
