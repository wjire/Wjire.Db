using Microsoft.Extensions.Options;

namespace Wjire.Dapper
{
    public class DbContext
    {
        public string Read { get; set; }
        public string Write { get; set; }

        public DbContext(IOptions<ConnectionOptions> options)
        {
            ConnectionOptions connection = options.Value;
            Read = connection.Read;
            Write = connection.Write;
        }

        public IUnitOfWork CreateTransaction()
        {
            return new TransactionConnection(Write);
        }

        public IUnitOfWork CreateSingleConnection()
        {
            return new SingleConnection(Read);
        }
    }
}