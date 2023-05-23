using System.ComponentModel.DataAnnotations;

namespace ProFlow.Models
{
    public class ApplicationUserToAssignments
    {
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int AssignmentsId { get; set; }
        public Assignments? Assignments { get; set; }
    }
}
