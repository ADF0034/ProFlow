using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFlow.Data.Migrations;
using ProFlow.Models;

namespace ProFlow.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; set; }



        public async Task<IActionResult> Index()
        {
            //await GenerateNormalRole();
            //await GenerateAdmin();
            List<Users> UserS = new List<Users>();
            var user = _userManager.GetUserName(User);
            var users = _userManager.Users.ToList();
            foreach (var item in users)
            {

                //item.UserRoles = new List<IdentityRole>();
                var roles = await _userManager.GetRolesAsync(item);
                if (roles.Count != 0)
                {
                    UserS.Add(new Users { Id = item.Id, Name = item.UserName, Role = roles.FirstOrDefault() });

                    //item.UserRoles.Add(new IdentityRole("Administrator"));
                }
                else
                {
                    UserS.Add(new Users { Id = item.Id, Name = item.UserName, Role = roles.FirstOrDefault() });
                }
            }
            var test = UserS;
            return View(UserS);
        }
        public async Task<IActionResult> UpdateUserRole(string UserId, string RoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var role = await _userManager.GetRolesAsync(user);
            foreach (var item in role)
            {
                var removeroles = await _userManager.RemoveFromRoleAsync(user, item);
            }

            var roles = _roleManager.Roles.Where(x => x.Id == RoleId);
            var roleresult = await _userManager.AddToRoleAsync(user, roles.FirstOrDefault().Name);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var role = await _userManager.GetRolesAsync(user);
            foreach (var item in role)
            {
                var removeroles = await _userManager.RemoveFromRoleAsync(user, item);
            }
            var res = await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
        //private async Task GenerateAdmin()
        //{

        //    IdentityRole identityRole = new IdentityRole
        //    {
        //        Name = "Administrator",
        //    };
        //    IdentityResult result = await _roleManager.CreateAsync(identityRole);
        //    if (result.Succeeded)
        //    {
        //        var yes = "yes";
        //    }
        //    else
        //    {
        //        var no = "no";
        //    }
        //}
        //private async Task GenerateNormalRole()
        //{
        //    IdentityRole identityRole = new IdentityRole
        //    {
        //        Name = "NormalUser",
        //    };
        //    IdentityResult result = await _roleManager.CreateAsync(identityRole);
        //    if (result.Succeeded)
        //    {
        //        var yes ="yes";
        //    }
        //    else
        //    {
        //        var no ="no";
        //    }
        //}

    }
}
