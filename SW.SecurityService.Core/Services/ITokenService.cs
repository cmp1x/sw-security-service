namespace SW.SecurityService.Core.Services
{
    using StackExchange.Redis;

    public interface ITokenService
    {
        string Get(string token);
        IDatabase Set(string token, string user);
    }
}