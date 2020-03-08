using System;
using System.Data;

namespace Wjire.Dapper
{
    public static class ConnectionHelper
    {
        internal static Func<string, IDbConnection> ConnectionFactory { get; }

        static ConnectionHelper()
        {
            IConnectionFactoryProvider connectionFactoryProvider = ServiceCollectionExtensions.GetRequiredService<IConnectionFactoryProvider>();
            ConnectionFactory = connectionFactoryProvider.ConnectionFactory;
        }
    }
}
