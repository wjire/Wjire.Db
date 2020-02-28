using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Dapper.SqlServer.CAP
{
    public class CapDbContextDbOptionsExtension : IDbOptionsExtension
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<CapDbContext>();
        }
    }
}
