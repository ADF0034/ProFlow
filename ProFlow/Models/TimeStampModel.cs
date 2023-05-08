using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProFlow.Models
{
    public class TimeStampModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Indtastede tid")]
        public decimal Time { get; set; }
        [StringLength(100)]
        [DisplayName("Beskrivelse")]
        public string Description { get; set; }

        public Guid CurrentAssignment { get; set; }
        public Assignments Assignments { get; set; }
    }
}
