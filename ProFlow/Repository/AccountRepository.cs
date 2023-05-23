using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProFlow.Data.Migrations;
using ProFlow.Models;
using ProFlow.Models.APIModels;

namespace ProFlow.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
            };

            var res = await _userManager.CreateAsync(user, signUpModel.Password);

            var roles = _roleManager.Roles.Where(x => x.Id == "b9179de7-bc69-49a2-86b6-44ea9553f658");
            var roleresult = await _userManager.AddToRoleAsync(user, roles.FirstOrDefault().Name);
            return res;

        }
        public async Task<UserIdentityModel> LoginAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            UserIdentityModel identity = new UserIdentityModel();
            identity.UserId = "";
            identity.Token = "";
            return identity;
        }
    }
}
