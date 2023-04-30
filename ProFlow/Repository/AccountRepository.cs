using Microsoft.AspNetCore.Identity;
using ProFlow.Models;

namespace ProFlow.Repository
{
    public class AccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            return res;

        }
    }
}
