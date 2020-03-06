using System;
using System.Data;

namespace Wjire.Dapper
{
    /// <summary>
    /// 单连接读取数据
    /// </summary>
    public class SingleConnection : IUnitOfWork
    {

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }
        public string ConnectionString { get; set; }

        public SingleConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}