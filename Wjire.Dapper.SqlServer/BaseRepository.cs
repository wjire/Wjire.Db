using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Wjire.Dapper.SqlServer
{
    public class BaseRepository<TEntity> : DapperBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(string connectionString) : base(connectionString)
        {
        }

        public BaseRepository(IUnitOfWork unit) : base(unit)
        {
        }


        /// <summary>
        /// 添加单条
        /// </summary>
        /// <param name="entity"></param>
        protected override void Insert(TEntity entity)
        {
            string sql = SqlHelper.GetInsertSql(entity);
            int result = Connection.Execute(sql, entity, Transaction);
            if (result != 1)
            {
                throw new Exception("Insert throw an exception");
            }
        }
        

        /// <summary>
        /// 添加单条记录,并返回新增记录的自增键的值.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>新增数据的主键</returns>
        protected override long InsertAndReturnIdentity(TEntity entity)
        {
            string sql = SqlHelper.GetInsertSql(entity);
            sql += "SELECT CAST(SCOPE_IDENTITY() as bigint);";
            return Connection.ExecuteScalar<long>(sql, entity, Transaction);
        }


        /// <summary>
        /// 存在更新,不存在插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        /// <param name="updateField">如果存在,需要更新的字段</param>
        protected override void InsertOrUpdate(TEntity entity, object whereField, object updateField)
        {
            Type type = entity.GetType();
            string tableName = type.Name;
            string addSql = SqlHelper.GetInsertSql(type, tableName);
            string whereSql = SqlHelper.GetWhereSql(whereField);
            string updateSql = SqlHelper.GetUpdateSql(updateField);
            string sql = $" IF EXISTS (SELECT TOP 1 1 FROM {tableName} {whereSql}) {updateSql} {whereSql} ELSE {addSql};";
            int result = Connection.Execute(sql, entity, Transaction);
            if (result != 1)
            {
                throw new Exception("InsertOrUpdate throw an exception");
            }
        }


        /// <summary>
        /// 如果不存在则插入,否则忽略
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        protected override void InsertIfNotExists(TEntity entity, object whereField)
        {
            Type type = entity.GetType();
            string tableName = type.Name;
            string addSql = SqlHelper.GetInsertSql(type, tableName);
            string whereSql = SqlHelper.GetWhereSql(whereField);
            string sql = $" IF NOT EXISTS (SELECT TOP 1 1 FROM {tableName} {whereSql}) {addSql};";
            Connection.Execute(sql, entity, Transaction);
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dataSql">查询数据的语句,确保已经排序</param>
        /// <param name="countSql">查询总记录数的语句</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataCount">总记录数</param>
        /// <param name="param">参数</param>
        /// <returns>not null</returns>
        protected override IEnumerable<T> Page<T>(string dataSql, string countSql, int pageIndex, int pageSize, out int pageCount,
            out int dataCount, object param = null)
        {
            dataCount = Connection.ExecuteScalar<int>(countSql, param);
            if (dataCount == 0)
            {
                pageCount = 0;
                return Enumerable.Empty<T>();
            }
            pageCount = dataCount % pageSize == 0
                ? dataCount / pageSize
                : dataCount / pageSize + 1;
            string sql = dataSql + $" OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY ";
            return Connection.Query<T>(sql, param, Transaction);
        }
    }
}
