namespace SW.SecurityService.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;
    using SW.SecurityService.Core.Services;

    public static class ExtensionService
    {
        public static void AddCoreService(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ITokenService, RedisService>();

            services.AddSingleton<IConnectionMultiplexer>(
                c => ConnectionMultiplexer.Connect(connectionString));
        }
    }
}
