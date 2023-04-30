using Microsoft.AspNetCore.Identity;
using ProFlow.Models;

namespace ProFlow.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
    }
}
