using System;
using System.Data;

namespace Wjire.Db.Dapper.SqlServer
{
    /// <summary>
    /// 单连接读取数据
    /// </summary>
    public class SingleConnection : IUnitOfWork
    {

        /// <summary>
        /// IDbConnection
        /// </summary>
        public IDbConnection Connection { get; set; }


        /// <summary>
        /// IDbTransaction
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        public Func<IDbConnection> ConnectionFactory { get; }
        public Func<IDbConnection, IDbTransaction> TransactionFactory { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        public SingleConnection(string name)
        {
            ConnectionFactory = ConnectionFactoryProvider.GetConnectionFactory(name);
        }


        /// <summary>
        /// 提交事物
        /// </summary>
        public void Commit()
        {
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
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