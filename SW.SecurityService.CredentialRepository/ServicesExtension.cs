namespace SW.SecurityService.CredentialRepository
{
    using Microsoft.Extensions.DependencyInjection;
    using SW.SecurityService.CredentialRepository.Repository;

    public static class ServicesExtension
    {
        public static void AddRepositoryServices(this IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICredentialRepository, CredentialRepository>(provider => new CredentialRepository(connectionString));
        }
    }
}
