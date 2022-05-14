namespace SW.SecurityService.Core.Providers
{
    using System;

    public class TokenProvider : ITokenProvider
    {
        public string GetNewToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
