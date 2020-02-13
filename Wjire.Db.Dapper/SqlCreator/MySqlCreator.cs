using System;
using System.Text;

namespace Wjire.Db.Dapper.SqlCreator
{
    internal class MySqlCreator : AbstractSqlCreator
    {
        internal static Lazy<AbstractSqlCreator> Singleton = new Lazy<AbstractSqlCreator>(() => new MySqlCreator());

        private MySqlCreator()
        {

        }
        
        internal override string GetQueryPageSql(StringBuilder dataSql, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
