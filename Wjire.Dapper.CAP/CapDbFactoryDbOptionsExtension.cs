using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Dapper.CAP
{
    public class CapDbFactoryDbOptionsExtension : IDbOptionsExtension
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<CapDbContext>();
        }
    }
}
