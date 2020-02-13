using System;
using MySql.Data.MySqlClient;
using Wjire.Db.Dapper.SqlCreator;

namespace Wjire.Db.Dapper
{
    internal class MySqlConnectionWrapper : AbstractConnectionWrapper
    {
        public MySqlConnectionWrapper(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        internal override Lazy<AbstractSqlCreator> SqlCreatorFactory => MySqlCreator.Singleton;
    }
}
