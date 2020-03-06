using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceProvider ServiceProvider { get; set; }

        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DbOptions> dbOptionsAction)
        {
            services.AddSingleton<DbContext>();
            DbOptions options = new DbOptions();
            dbOptionsAction(options);
            foreach (IDbOptionsExtension serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            services.Configure(dbOptionsAction);
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }

        //反模式,好难受
        public static T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
