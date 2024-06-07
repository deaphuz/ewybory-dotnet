using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        [ForeignKey("Voter")]
        public int VoterId { get; set; }
        public Voter Voter { get; set; }

        [ForeignKey("Election")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }
    }
}
