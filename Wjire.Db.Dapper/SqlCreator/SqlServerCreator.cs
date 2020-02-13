using System;
using System.Text;

namespace Wjire.Db.Dapper.SqlCreator
{
    internal class SqlServerCreator : AbstractSqlCreator
    {
        internal static Lazy<AbstractSqlCreator> Singleton = new Lazy<AbstractSqlCreator>(() => new SqlServerCreator());

        private SqlServerCreator()
        {

        }

        /// <summary>
        /// 获取分页查询的sql语句
        /// </summary>
        /// <param name="dataSql">查询数据的SQL语句</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        internal override string GetQueryPageSql(StringBuilder dataSql, int pageSize, int pageIndex)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            dataSql.Append($" OFFSET {startIndex} ROWS FETCH NEXT {pageSize} ROWS ONLY ");
            return dataSql.ToString();
        }
    }
}
