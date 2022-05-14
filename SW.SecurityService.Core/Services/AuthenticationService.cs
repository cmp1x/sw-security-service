namespace SW.SecurityService.Core.Services
{
    using SW.SecurityService.CredentialRepository.Repository;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICredentialRepository credentialsRepository;

        public AuthenticationService(ICredentialRepository credentialsRepository)
        {
            this.credentialsRepository = credentialsRepository;
        }

        public bool IsValidCredentials(string user, string password)
        {
            var actualCredentials = this.credentialsRepository.GetCredential(user);

            if (password == actualCredentials.Password)
            {
                return true;
            }

            return false;
        }
    }
}
