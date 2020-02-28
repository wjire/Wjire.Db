using System;
using System.Data;
using Wjire.Db.Dapper.SqlServer.Connection;

namespace Wjire.Db.Dapper
{
    /// <summary>
    /// 单连接读取数据
    /// </summary>
    public class SingleConnection : IUnitOfWork
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
        public SingleConnection(string name, IConnectionFactoryProvider provider)
        {
            ConnectionStringName = name;
            ConnectionFactoryProvider = provider;
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