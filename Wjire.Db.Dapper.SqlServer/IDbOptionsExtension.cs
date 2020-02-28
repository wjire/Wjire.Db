using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Db.Dapper.SqlServer
{
    public interface IDbOptionsExtension
    {
        void AddServices(IServiceCollection services);
    }


    public class DbFactoryDbOptionsExtension : IDbOptionsExtension
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<DbFactory>();
        }
    }
}
