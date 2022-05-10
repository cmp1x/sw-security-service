using System.Collections.Generic;

namespace SW.SecurityService.Core.Services
{
    public interface ITokenService
    {
        string Get(string token);
        IDictionary<string, string> Set(string token, string user);
    }
}