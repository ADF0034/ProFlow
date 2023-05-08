using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProFlow.Models
{
    public class ProjectModel
    {
        [Key]
        public Guid ProjectId { get; set; }
        [DisplayName("Projct Name")]
        [Required]
        public string ProjctName { get; set; }
        [Display(Name = "Oprettelse dato")]
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name = "Tids requestering")]
        public bool TimeEntry { get; set; }
        [DisplayName("Slut dato")]
        public DateTime EndDate { get; set; }
        [DisplayName("Budget")]
        public decimal Budget { get; set; }
        [DisplayName("Ejer")]
        public string Owner { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "skal være mellem 5 og 255 karakter", MinimumLength = 5)]
        public string Password { get; set; }

        public ICollection<Assignments>? assignments { get; set; }
        public ICollection<MaterialModel>? materials { get; set; }
    }
}
