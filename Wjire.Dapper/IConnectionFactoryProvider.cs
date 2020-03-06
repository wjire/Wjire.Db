using System;
using System.Data;

namespace Wjire.Dapper
{
    public interface IConnectionFactoryProvider
    {
        Func<string, IDbConnection> ConnectionFactory { get; }
    }
}
