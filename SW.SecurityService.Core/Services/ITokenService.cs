namespace SW.SecurityService.Core.Services
{
    using StackExchange.Redis;

    public interface ITokenService
    {
        string Get(string token);
        bool Set(string token, string user);
    }
}