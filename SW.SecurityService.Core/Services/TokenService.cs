namespace SW.SecurityService.Core.Services
{
    using System.Collections.Generic;

    public class TokenService : ITokenService
    {
        private readonly IDictionary<string, string> userTokens;

        public TokenService()
        {
            this.userTokens = new Dictionary<string, string>();
        }

        public string Get(string token)
        {
            if (this.userTokens.ContainsKey(token))
            {
                return this.userTokens[token];
            }

            return null;
        }

        public IDictionary<string, string> Set(string token, string user)
        {
            this.userTokens.Add(token, user);

            return this.userTokens;
        }
    }
}
