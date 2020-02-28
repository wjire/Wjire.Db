using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Dapper
{
    public interface IDbOptionsExtension
    {
        void AddServices(IServiceCollection services);
    }


    public class DbContextDbOptionsExtension : IDbOptionsExtension
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<DbContext>();
        }
    }
}
