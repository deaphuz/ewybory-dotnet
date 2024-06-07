using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ewybory_dotnet.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
