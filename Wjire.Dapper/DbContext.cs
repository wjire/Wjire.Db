using Microsoft.Extensions.Options;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Dapper
{
    public class DbContext
    {
        protected ConnectionOptions ConnectionOptions { get; }

        public DbContext(IOptions<ConnectionOptions> options)
        {
            ConnectionOptions = options.Value;
        }

        public IUnitOfWork Transaction => new TransactionConnection(ConnectionOptions.Write);

        public IUnitOfWork Single => new SingleConnection(ConnectionOptions.Read);
    }
}