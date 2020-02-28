using System;
using System.Data;

namespace Wjire.Db.Dapper.SqlServer.Connection
{
    public interface IConnectionFactoryProvider
    {
        /// <summary>
        /// 获取连接工厂
        /// </summary>
        /// <returns></returns> 
        Func<string, IDbConnection> ConnectionFactory { get; }
    }
}
