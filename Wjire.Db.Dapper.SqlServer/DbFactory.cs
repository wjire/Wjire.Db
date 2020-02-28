using Microsoft.Extensions.Options;
using Wjire.Db.Dapper.SqlServer.Connection;

namespace Wjire.Db.Dapper.SqlServer
{

    /// <summary>
    /// 数据库 nCoV 连接工厂
    /// </summary>
    public class DbFactory : IDbFactory
    {
        protected readonly IConnectionFactoryProvider Provider;
        protected readonly IOptions<ConnectionOptions> Options;


        public DbFactory(IConnectionFactoryProvider provider, IOptions<ConnectionOptions> options)
        {
            Provider = provider;
            Options = options;
        }


        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork CreateTransaction()
        {
            return new TransactionConnection(Options.Value.Write, Provider);
        }


        /// <summary>
        /// 创建单连接
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork CreateSingle()
        {
            return new SingleConnection(Options.Value.Read, Provider);
        }
    }
}