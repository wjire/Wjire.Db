using System;
using System.Configuration;
using System.Data;

namespace Wjire.Db.Dapper
{

    /// <summary>
    /// 连接工厂
    /// </summary>
    internal class ConnectionWrapperFactory
    {

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns> 
        internal static AbstractConnectionWrapper GetConnectionWrapper(string name)
        {
            ConnectionStringSettings settings = ConnectionStringHelper.GetConnectionStringSettings(name);
            AbstractConnectionWrapper wrapper = CreateConnectionWrapper(settings);
            if (wrapper.Connection.State != ConnectionState.Open)
            {
                wrapper.Connection.Open();
            }
            return wrapper;
        }


        private static AbstractConnectionWrapper CreateConnectionWrapper(ConnectionStringSettings settings)
        {
            switch (settings.ProviderName.ToLower())
            {
                case "sqlserver":
                    return new SqlConnectionWrapper(settings.ConnectionString);
                case "mysql":
                    return new MySqlConnectionWrapper(settings.ConnectionString);
                default: throw new ArgumentException($"尚不支持 {settings.ProviderName} 数据库");
            }
        }
    }
}
