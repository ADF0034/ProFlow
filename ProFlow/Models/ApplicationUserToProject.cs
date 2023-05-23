using System.Reflection.Metadata.Ecma335;

namespace ProFlow.Models
{
    public class ApplicationUserToProject
    {
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectModel? project { get; set; }
        public int? roleid { get; set; }
        public Roles role { get; set; }
    }
}
