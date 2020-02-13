using System;
using System.Data;
using Wjire.Db.Dapper.SqlCreator;

namespace Wjire.Db.Dapper
{
    public abstract class AbstractConnectionWrapper
    {
        internal abstract Lazy<AbstractSqlCreator> SqlCreatorFactory { get; }
        internal IDbConnection Connection { get; set; }
        internal IDbTransaction Transaction { get; set; }
    }
}
