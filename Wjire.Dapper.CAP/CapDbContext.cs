using DotNetCore.CAP;
using Microsoft.Extensions.Options;
using Wjire.Dapper.Connection;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Dapper.CAP
{
    public class CapDbContext : DbContext
    {

        public CapDbContext(IConnectionFactoryProvider provider, IOptions<ConnectionOptions> options) : base(provider, options)
        {
        }


        public IUnitOfWork CreateCapTransaction(ICapPublisher cap, bool autoCommit = false)
        {
            return new CapTransaction(Options.Value.Write, Provider, cap, autoCommit);
        }

    }
}