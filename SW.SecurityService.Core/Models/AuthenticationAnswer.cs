namespace SW.SecurityService.Core.Models
{
    public class AuthenticationAnswer
    {
        public UserRedis userRedis { get; set; }

        public bool wrongPassword { get; set; } = false;

        public bool nonExistenLogin { get; set; } = false;
    }
}
