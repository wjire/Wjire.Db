using System;
using System.Data;
using DotNetCore.CAP;
using Wjire.Dapper.Connection;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Dapper.CAP
{
    /// <summary>
    /// CAP事务操作
    /// </summary>	
    public class CapTransaction : IUnitOfWork
    {

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }
        public IConnectionFactoryProvider ConnectionFactoryProvider { get; set; }
        public string ConnectionString { get; set; }


        public CapTransaction(string name, IConnectionFactoryProvider provider, ICapPublisher cap, bool autoCommit)
        {
            ConnectionString = name;
            ConnectionFactoryProvider = provider;
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