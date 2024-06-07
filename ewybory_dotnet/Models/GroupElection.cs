using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class GroupElection
    {
        [Key]
        public int GroupElectionId { get; set; }

        [ForeignKey("Election")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
