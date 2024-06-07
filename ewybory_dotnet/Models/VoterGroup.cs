using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class VoterGroup
    {
        [Key]
        public int VoterGroupId { get; set; }

        [ForeignKey("Voter")]
        public int VoterId { get; set; }
        public Voter Voter { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
