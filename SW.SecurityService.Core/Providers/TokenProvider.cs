namespace SW.SecurityService.Core.Providers
{
    using System;

    public class TokenProvider : ITokenProvider
    {
        public string NewGuidInString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
