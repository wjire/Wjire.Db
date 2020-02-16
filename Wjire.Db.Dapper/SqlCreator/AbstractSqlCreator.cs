using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Wjire.Db.Dapper.SqlCreator
{
    internal abstract class AbstractSqlCreator
    {

        protected readonly ConcurrentDictionary<Type, string> AddSqlContainer = new ConcurrentDictionary<Type, string>();
        protected readonly ConcurrentDictionary<Type, string> UpdateSqlContainer = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// 获取数据库插入数据时的sql语句
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal virtual string GetInsertSql(object obj)
        {
            Type type = obj.GetType();
            string sql = AddSqlContainer.GetOrAdd(type, t =>
            {
                StringBuilder sqlBuilder = new StringBuilder(128);
                sqlBuilder.Append($" INSERT INTO {type.Name} ");
                StringBuilder addBuilder = new StringBuilder(64);
                foreach (PropertyInfo property in type.GetProperties())
                {
                    //忽略自增字段
                    KeyAttribute keyAttribute = property.GetCustomAttribute<KeyAttribute>();
                    if (keyAttribute == null)
                    {
                        addBuilder.Append($"@{property.Name},");
                    }
                }
                string paramString = addBuilder.Remove(addBuilder.Length - 1, 1).ToString();
                string fieldString = paramString.Replace('@', ' ');
                sqlBuilder.Append($"({fieldString}) VALUES ({paramString});");
                return sqlBuilder.ToString();
            });
            return sql;
        }


        /// <summary>
        /// 获取数据库更新数据时的sql语句
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal virtual string GetUpdateSql(object obj)
        {
            Type type = obj.GetType();
            string result = UpdateSqlContainer.GetOrAdd(type, t =>
            {
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append($" UPDATE {type.Name} SET ");

                foreach (PropertyInfo property in type.GetProperties())
                {
                    //忽略自增字段
                    KeyAttribute att = property.GetCustomAttribute<KeyAttribute>();
                    if (att != null)
                    {
                        continue;
                    }
                    sqlBuilder.Append($"{property.Name}=@{property.Name},");
                }
                return sqlBuilder.Remove(sqlBuilder.Length - 1, 1).ToString();
            });
            return result;
        }

        internal virtual string GetAddOrUpdateSql(object obj, string whereSql)
        {
            string tableName = obj.GetType().Name;
            string addSql = GetInsertSql(obj);
            string updateSql = GetUpdateSql(obj);
            string sql = $" IF EXISTS (SELECT TOP 1 1 FROM {tableName} {whereSql}) {updateSql} {whereSql} ELSE {addSql};";
            return sql;
        }


        /// <summary>
        /// 获取分页查询的sql语句
        /// </summary>
        /// <param name="dataSql">查询数据的sql语句</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        internal abstract string GetQueryPageSql(StringBuilder dataSql, int pageSize, int pageIndex);
    }
}
