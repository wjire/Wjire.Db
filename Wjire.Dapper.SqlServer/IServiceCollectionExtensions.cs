using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Dapper.SqlServer
{

    public class SqlServerDbOptionsExtension : IDbOptionsExtension
    {
        private readonly Action<ConnectionOptions> _configure;

        public SqlServerDbOptionsExtension(Action<ConnectionOptions> configure)
        {
            _configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactoryProvider, SqlServerConnectionFactoryProvider>();
            services.Configure(_configure);
        }
    }
}
