using System;
using System.Data;

namespace Wjire.Db.Dapper.SqlServer
{
    /// <summary>
    /// 事务操作
    /// </summary>	
    public class TransactionConnection : IUnitOfWork
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }

        public Func<IDbConnection> ConnectionFactory { get; set; }

        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <param name="isolationLevel">事务级别</param>
        public TransactionConnection(string name, IsolationLevel? isolationLevel = null)
        {
            ConnectionFactory = ConnectionFactoryProvider.GetConnectionFactory(name);
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
