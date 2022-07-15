using SW.SecurityService.Core.Enums;

namespace SW.SecurityService.Core.Models
{
    public class AuthenticationAnswer
    {
        public UserRedis userRedis { get; set; }

        public AuthenticationAnswerStatus Status { get; set; }
    }
}
