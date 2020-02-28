using DotNetCore.CAP;
using Microsoft.Extensions.Options;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Dapper.SqlServer.CAP
{
    public class CapDbContext : DbContext
    {

        public CapDbContext(IOptions<ConnectionOptions> options) : base(options)
        {
        }


        public IUnitOfWork CreateCapTransaction(ICapPublisher cap, bool autoCommit = false)
        {
            return new CapTransaction(ConnectionOptions.Write, cap, autoCommit);
        }

    }
}