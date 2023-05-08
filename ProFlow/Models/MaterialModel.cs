using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProFlow.Models
{
    public class MaterialModel
    {
        [Key]
        public int id { get; set; }
        [DisplayName("navn")]
        public string? Name { get; set; }
        [DisplayName("Antal")]
        public int amount { get; set; }
        [DisplayName("Pris")]
        public decimal price { get; set; }
        [DisplayName("Hjemmside")]
        public string? url { get; set; }

        [ForeignKey("ProjectModel")]
        public Guid ProjectId { get; set; }
        public ProjectModel ProjectModel { get; set; } 
    }
}
