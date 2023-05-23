using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using ProFlow.Data;
using ProFlow.Models;
using System.Security.Claims;

namespace ProFlow.Repository
{
    public class ProjektRepository
    {
        private readonly ApplicationDbContext _context; 
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjektRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<List<ProjectModel>> Projects(ClaimsPrincipal claimsPrincipal)
        {
            var userid = _userManager.GetUserId(claimsPrincipal);
            if (userid != null)
            {
                List<ProjectModel> projects = new List<ProjectModel>();
                var projectse = _context.userToProjects.Where(x => x.UserId == userid);
                foreach (var item in projectse) 
                {
                    projects.Add(item.project);
                }
                return projects;
            }
            else 
            {
                return new List<ProjectModel>(); 
            }


        }

    }
}
