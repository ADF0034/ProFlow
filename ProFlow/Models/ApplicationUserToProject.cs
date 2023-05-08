namespace ProFlow.Models
{
    public class ApplicationUserToProject
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectModel project { get; set; }
    }
}
