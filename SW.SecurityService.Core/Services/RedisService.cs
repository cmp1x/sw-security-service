namespace SW.SecurityService.Core.Services
{
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RedisService : ITokenService
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public RedisService (IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
        }

        public string Get(string token)
        {
            var db = this.connectionMultiplexer.GetDatabase();
            return db.StringGet(token);
        }

        public bool Set(string token, string user)
        {
            var db = this.connectionMultiplexer.GetDatabase();
            var isSet = db.StringSet(token, user);

            return isSet;
        }
    }
}
