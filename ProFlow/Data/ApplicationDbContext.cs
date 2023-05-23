using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProFlow.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUserToProject> userToProjects { get; set; }
        public DbSet<ApplicationUserToAssignments> UserToAssignments { get; set; }
        public DbSet<UserRolesToRoles> userRolesToRoles { get; set; }
        public DbSet<Roles> roles { get; set; }
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
            builder.Entity<ApplicationUserToProject>().HasKey(ir => new { ir.UserId, ir.ProjectId,ir.roleid });
            builder.Entity<ApplicationUserToAssignments>().HasKey(ir => new { ir.UserId, ir.AssignmentsId });
            builder.Entity<UserRolesToRoles>().HasKey(ir => new { ir.UserRolesId, ir.Roleid });
        }
    }
}