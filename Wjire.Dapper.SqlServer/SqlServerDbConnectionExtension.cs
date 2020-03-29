using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Wjire.Dapper.SqlServer
{
    public static class SqlServerDbConnectionExtension
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        public static void Add(this IDbConnection connection, object entity, IDbTransaction transaction)
        {
            string sql = SqlHelper.GetInsertSql(entity);
            int addResult = connection.Execute(sql, entity, transaction);
            if (addResult != 1)
            {
                throw new Exception("add throw an exception");
            }
        }


        /// <summary>
        /// 添加单条记录,并返回新增记录的自增键的值.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns>新增数据的主键</returns>
        public static long AddAndReturnIdentity(this IDbConnection connection, object entity, IDbTransaction transaction)
        {
            string sql = SqlHelper.GetInsertSql(entity);
            sql += "SELECT CAST(SCOPE_IDENTITY() as bigint);";
            return connection.ExecuteScalar<long>(sql, entity, transaction);
        }



        /// <summary>
        /// 存在更新,不存在插入
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        /// <param name="transaction"></param>
        public static void AddOrUpdate(this IDbConnection connection, object entity, object whereField, IDbTransaction transaction)
        {
            AddOrUpdate(connection, entity, whereField, entity, transaction);
        }


        /// <summary>
        /// 存在更新,不存在插入
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        /// <param name="updateField">如果存在,需要更新的字段</param>
        /// <param name="transaction"></param>
        public static void AddOrUpdate(this IDbConnection connection, object entity, object whereField, object updateField, IDbTransaction transaction)
        {
            Type type = entity.GetType();
            string tableName = type.Name;
            string addSql = SqlHelper.GetInsertSql(type, tableName);
            string whereSql = SqlHelper.GetWhereSql(whereField);
            string updateSql = SqlHelper.GetUpdateSql(updateField);
            string sql = $" IF EXISTS (SELECT TOP 1 1 FROM {tableName} {whereSql}) {updateSql} {whereSql} ELSE {addSql};";
            int result = connection.Execute(sql, entity, transaction);
            if (result != 1)
            {
                throw new Exception("AddOrUpdate throw an exception");
            }
        }


        /// <summary>
        /// 如果不存在则插入,否则忽略
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        /// <param name="transaction"></param>
        public static void AddIfNotExists(this IDbConnection connection, object entity, object whereField, IDbTransaction transaction)
        {
            Type type = entity.GetType();
            string tableName = type.Name;
            string addSql = SqlHelper.GetInsertSql(type, tableName);
            string whereSql = SqlHelper.GetWhereSql(whereField);
            string sql = $" IF NOT EXISTS (SELECT TOP 1 1 FROM {tableName} {whereSql}) {addSql};";
            connection.Execute(sql, entity, transaction);
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dataSql">查询数据的语句,确保已经排序</param>
        /// <param name="countSql">查询总记录数的语句</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataCount">总记录数</param>
        /// <param name="param">参数</param>
        /// <param name="transaction"></param>
        /// <returns>not null</returns>
        public static IEnumerable<TEntity> QueryPage<TEntity>(this IDbConnection connection, string dataSql, string countSql, int pageIndex, int pageSize, out int pageCount, out int dataCount, IDbTransaction transaction, object param = null)
        {
            dataCount = connection.ExecuteScalar<int>(countSql, param);
            if (dataCount == 0)
            {
                pageCount = 0;
                return Enumerable.Empty<TEntity>();
            }
            pageCount = dataCount % pageSize == 0
                ? dataCount / pageSize
                : dataCount / pageSize + 1;
            string sql = dataSql + $" OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY ";
            return connection.Query<TEntity>(sql, param, transaction);
        }
    }
}
