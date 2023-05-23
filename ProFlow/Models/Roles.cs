using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProFlow.Models
{
    public class Roles
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Description")]
        [StringLength(100, MinimumLength = 5)]
        public string Beskrivelse { get; set; }

    }
}
