using Microsoft.Extensions.DependencyInjection;

namespace Wjire.Dapper
{
    public interface IDbOptionsExtension
    {
        void AddServices(IServiceCollection services);
    }
}
