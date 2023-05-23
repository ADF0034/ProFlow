using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProFlow.Models
{
    public class UserRoles
    {
        [Key]
        public int id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("ProjectModel")]
        public Guid ProjectId { get; set; }
        public ProjectModel ProjectModel { get; set; }


    }
}
