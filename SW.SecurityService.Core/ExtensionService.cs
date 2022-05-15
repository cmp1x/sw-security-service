namespace SW.SecurityService.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;
    using SW.SecurityService.Core.Providers;
    using SW.SecurityService.Core.Services;

    public static class ExtensionService
    {
        public static void AddCoreServices(this IServiceCollection services, string redisConnection)
        {
            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddSingleton<ITokenService, RedisService>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IConnectionMultiplexer>(
                c => ConnectionMultiplexer.Connect(redisConnection));
        }
    }
}
