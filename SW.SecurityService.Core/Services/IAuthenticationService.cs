using SW.SecurityService.Core.Models;

namespace SW.SecurityService.Core.Services
{
    public interface IAuthenticationService
    {
        UserRedis AuthenticateUser(string userName, string password);
    }
}