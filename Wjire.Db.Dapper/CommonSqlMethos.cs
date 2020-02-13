using System;
using System.Collections.Generic;
using System.Text;

namespace Wjire.Db.Dapper
{
    public partial class BaseRepository<TEntity>
    {

        public void Add(TEntity entity)
        {
            string sql = SqlCreator.GetInsertSql(entity);
            int addResult = Execute(sql, entity);
            if (addResult != 1)
            {
                throw new Exception("insert into database throw a exception");
            }
        }

        public void Add(List<TEntity> entities)
        {
            string sql = SqlCreator.GetInsertSql(entities[0]);
            int addResult = Execute(sql, entities);
            if (addResult != entities.Count)
            {
                throw new Exception("insert into database throw a exception");
            }
        }


        /// <summary>
        /// 存在更新,不存在插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereSql">判断是否存在的where语句</param>
        /// <param name="param"></param>
        /// <returns>受影响的行数</returns>
        protected void AddOrUpdate(TEntity entity, string whereSql, object param = null)
        {
            var sql = SqlCreator.GetAddOrUpdateSql(entity, whereSql);
            var result = Execute(sql, param);
            if (result != 1)
            {
                throw new Exception("AddOrUpdate database throw a exception");
            }
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSql">查询数据的语句,确保已经排序</param>
        /// <param name="countSql">查询总记录数的语句</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="dataCount">总记录数</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        protected IEnumerable<T> QueryPage<T>(StringBuilder dataSql, string countSql, int pageSize, int pageIndex, out int pageCount, out int dataCount, object param = null)
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
            string sql = SqlCreator.GetQueryPageSql(dataSql, pageSize, pageIndex);
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
