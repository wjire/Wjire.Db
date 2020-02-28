using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Wjire.Dapper.Connection;

namespace Wjire.Dapper.SqlServer
{
    public class SqlServerConnectionFactoryProvider : IConnectionFactoryProvider
    {
        public Func<string, IDbConnection> ConnectionFactory => connectionString =>
        {
            IDbConnection connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        };
    }
}
