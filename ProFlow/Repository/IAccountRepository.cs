using Microsoft.AspNetCore.Identity;
using ProFlow.Models.APIModels;

namespace ProFlow.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
    }
}
