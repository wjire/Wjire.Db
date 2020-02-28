using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Db.Dapper.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DbOptions> dbOptionsAction)
        {
            DbOptions options = new DbOptions();
            dbOptionsAction(options);
            foreach (IDbOptionsExtension serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            services.Configure(dbOptionsAction);
            return services;
        }
    }


    public static class DbOptionsExtensions
    {
        public static DbOptions UseDbFactory(this DbOptions options)
        {
            options.RegisterExtension(new DbFactoryDbOptionsExtension());
            return options;
        }
    }
}
