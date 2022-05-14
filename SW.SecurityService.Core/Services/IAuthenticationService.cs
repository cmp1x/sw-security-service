namespace SW.SecurityService.Core.Services
{
    public interface IAuthenticationService
    {
        bool IsValidCredentials(string user, string password);
    }
}