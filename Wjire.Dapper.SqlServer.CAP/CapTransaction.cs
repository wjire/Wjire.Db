using System;
using System.Data;
using DotNetCore.CAP;

namespace Wjire.Dapper.SqlServer.CAP
{
    /// <summary>
    /// CAP事务操作
    /// </summary>	
    public class CapTransaction : IUnitOfWork
    {

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }
        public string ConnectionString { get; set; }


        public CapTransaction(string connectionString, ICapPublisher cap, bool autoCommit)
        {
            ConnectionString = connectionString;
            TransactionFactory = connection => connection.BeginTransaction(cap, autoCommit);
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