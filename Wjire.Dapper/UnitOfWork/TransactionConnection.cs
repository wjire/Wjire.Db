using System;
using System.Data;

namespace Wjire.Dapper
{
    /// <summary>
    /// 事务操作
    /// </summary>	
    public class TransactionConnection : IUnitOfWork
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public string ConnectionString { get; set; }
        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }


        public TransactionConnection(string connectionString, IsolationLevel? isolationLevel = null)
        {
            ConnectionString = connectionString;
            TransactionFactory = connection => isolationLevel.HasValue ? connection.BeginTransaction(isolationLevel.Value) : connection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
