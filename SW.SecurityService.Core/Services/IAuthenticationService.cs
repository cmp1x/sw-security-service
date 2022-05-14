namespace SW.SecurityService.Core.Services
{
    public interface IAuthenticationService
    {
        bool IsProperPassword(string user, string password);
    }
}