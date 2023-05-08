using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProFlow.Models;

namespace ProFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUserToProject> userToProjects { get; set; }
        public DbSet<Assignments> assignments { get; set; }
        public DbSet<MaterialModel> materials { get; set; }
        public DbSet<ProjectModel> projects { get; set; }
        public DbSet<TimeStampModel> timeStamps { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserToProject>().HasKey(ir => new { ir.UserId, ir.ProjectId });
        }
    }
}