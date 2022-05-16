namespace SW.SecurityService.Core.Models
{
    using System;

    public class UserRedis
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime TokenCreated { get; set; }

        public string CurrentToken { get; set; }
    }
}
