using System;
using System.Data;
using System.Data.SqlClient;

namespace Wjire.Db.Dapper.SqlServer.Connection
{
    public class ConnectionFactoryProvider
    {

        /// <summary>
        /// 获取连接工厂
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns> 
        internal static Func<IDbConnection> GetConnectionFactory(string name)
        {
            return () =>
            {
                string connectionString = ConnectionStringHelper.GetConnectionString(name);
                SqlConnection connection = new SqlConnection(connectionString);
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection;
            };
        }
    }
}
