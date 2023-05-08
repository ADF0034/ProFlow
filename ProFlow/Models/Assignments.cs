using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProFlow.Models
{
    public class Assignments
    {
        [Key]
        public int AssigmentID { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Description")]
        public string Beskrivelse { get; set; }
        [ForeignKey("ProjectModel")]
        public Guid ProjectId { get; set; }
        public ProjectModel ProjectModel { get; set; }
        public ICollection<TimeStampModel> timeStamps { get; set; }
    }
}
