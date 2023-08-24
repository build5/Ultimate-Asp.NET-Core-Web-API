using Microsoft.AspNetCore.Identity;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
}

