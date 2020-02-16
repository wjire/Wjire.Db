using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Wjire.Db.Dapper.SqlServer
{
    internal class SqlHelper
    {
        protected static readonly ConcurrentDictionary<Type, string> AddSqlContainer = new ConcurrentDictionary<Type, string>();
        protected static readonly ConcurrentDictionary<Type, string> UpdateSqlContainer = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// 获取数据库插入数据时的sql语句
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static string GetInsertSql(object obj)
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
        internal static string GetUpdateSql(object obj)
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
    }
}
