using System;
using System.Data.SqlClient;
using Wjire.Db.Dapper.SqlCreator;

namespace Wjire.Db.Dapper
{
    internal class SqlConnectionWrapper : AbstractConnectionWrapper
    {
        public SqlConnectionWrapper(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        internal override Lazy<AbstractSqlCreator> SqlCreatorFactory => SqlServerCreator.Singleton;
    }
}
