namespace SW.SecurityService.Core.Services
{
    using StackExchange.Redis;

    public interface ITokenService
    {
        string Get(string token);
        
        /// <summary>
        /// Setting key value pair into redis server.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userInfoJson"></param>
        /// <returns>Returns true if setting was succseed, false otherwise</returns>
        bool Set(string token, string userInfoJson);
    }
}