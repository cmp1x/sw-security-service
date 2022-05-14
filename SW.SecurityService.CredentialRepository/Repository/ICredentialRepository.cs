using SW.SecurityService.CredentialRepository.Models;

namespace SW.SecurityService.CredentialRepository.Repository
{
    public interface ICredentialRepository
    {
        void Create(CredentialsDb credential);
        CredentialsDb GetCredential(string user);
    }
}