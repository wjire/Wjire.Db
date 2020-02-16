using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Wjire.Db.Dapper.SqlServer
{

    /// <summary>
    /// 连接工厂
    /// </summary>
    internal class ConnectionFactory
    {

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns> 
        internal static IDbConnection GetConnection(string name)
        {
            var connectionString = ConnectionStringHelper.GetConnectionString(name);
            var connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
