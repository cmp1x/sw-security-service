using SW.SecurityService.Core.Models;

namespace SW.SecurityService.Core.Services
{
    public interface IAuthenticationService
    {
        AuthenticationAnswer AuthenticateUser(string userName, string password);
    }
}