using System;
using System.Data;

namespace Wjire.Dapper.Connection
{
    public interface IConnectionFactoryProvider
    {
        Func<string, IDbConnection> ConnectionFactory { get; }
    }
}
