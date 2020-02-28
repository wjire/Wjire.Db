using System;
using System.Data;
using Wjire.Db.Dapper.SqlServer.Connection;

namespace Wjire.Db.Dapper
{
    /// <summary>
    /// 事务操作
    /// </summary>	
    public class TransactionConnection : IUnitOfWork
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }
        public IConnectionFactoryProvider ConnectionFactoryProvider { get; set; }
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <param name="isolationLevel">事务级别</param>
        public TransactionConnection(string name, IConnectionFactoryProvider provider, IsolationLevel? isolationLevel = null)
        {
            ConnectionStringName = name;
            ConnectionFactoryProvider = provider;
            TransactionFactory = connection => isolationLevel.HasValue ? connection.BeginTransaction(isolationLevel.Value) : connection.BeginTransaction();
        }



        /// <summary>
        /// 提交事物
        /// </summary>
        public void Commit()
        {
            Transaction.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
            Transaction.Rollback();
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
