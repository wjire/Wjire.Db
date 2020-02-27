using System;
using System.Collections.Generic;

namespace Wjire.Db.Dapper
{
    public partial class BaseRepository<TEntity>
    {

        public virtual void Add(TEntity entity)
        {
            string sql = SqlHelper.GetInsertSql(entity);
            int addResult = Execute(sql, entity);
            if (addResult != 1)
            {
                throw new Exception("add a entity throw an exception");
            }
        }

        public virtual void Add(List<TEntity> entities)
        {
            string sql = SqlHelper.GetInsertSql(entities[0]);
            int addResult = Execute(sql, entities);
            if (addResult != entities.Count)
            {
                throw new Exception("add some entities throw an exception");
            }
        }


        /// <summary>
        /// 添加单条记录,并返回新增记录的自增键的值.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>新增数据的主键</returns>
        public virtual long AddAndReturnIdentity(TEntity entity)
        {
            string sql = SqlHelper.GetInsertSql(entity);
            sql += "SELECT CAST(SCOPE_IDENTITY() as bigint);";
            return ExecuteScalar<long>(sql, entity);
        }


        /// <summary>
        /// 存在更新,不存在插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereSql">判断是否存在的where语句</param>
        /// <param name="updateSql">如果存在,更新的sql语句</param>
        /// <param name="param"></param>
        /// <returns>受影响的行数</returns>
        protected void AddOrUpdate(TEntity entity, string whereSql, string updateSql = null, object param = null)
        {
            string addSql = SqlHelper.GetInsertSql(entity);
            if (string.IsNullOrWhiteSpace(updateSql))
            {
                updateSql = SqlHelper.GetUpdateSql(entity);
            }
            string sql = $" IF EXISTS (SELECT TOP 1 1 FROM {TableName} {whereSql}) {updateSql} {whereSql} ELSE {addSql};";
            int result = Execute(sql, param);
            if (result != 1)
            {
                throw new Exception("AddOrUpdate a entity throw an exception");
            }
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
        /// <returns></returns>
        protected IEnumerable<TEntity> QueryPage(string dataSql, string countSql, int pageIndex, int pageSize, out int pageCount, out int dataCount, object param = null)
        {
            return QueryPage<TEntity>(dataSql, countSql, pageIndex, pageSize, out pageCount, out dataCount, param);
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSql">查询数据的语句,确保已经排序</param>
        /// <param name="countSql">查询总记录数的语句</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataCount">总记录数</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        protected IEnumerable<T> QueryPage<T>(string dataSql, string countSql, int pageIndex, int pageSize, out int pageCount, out int dataCount, object param = null)
        {
            dataCount = ExecuteScalar<int>(countSql, param);
            if (dataCount == 0)
            {
                pageCount = 0;
                return new List<T>();
            }
            pageCount = dataCount % pageSize == 0
                ? dataCount / pageSize
                : dataCount / pageSize + 1;
            string sql = dataSql + $" OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY ";
            return Query<T>(sql, param);
        }


        /// <summary>
        /// = 1  or  in (1,2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>   
        /// <returns></returns>
        protected string InTheCollection<T>(IList<T> collection) where T : struct
        {
            return collection.Count == 1 ? $" = {collection[0]} " : $" in ({string.Join(",", collection)}) ";
        }


        /// <summary>
        /// = 'a'  or  in ('a','b')
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        protected string InTheCollection(IList<string> collection)
        {
            return collection.Count == 1 ? $" = '{collection[0]}' " : $" in ('{string.Join("','", collection)}') ";
        }
    }
}
