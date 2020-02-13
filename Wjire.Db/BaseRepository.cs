
using System;
using System.Data;

namespace Wjire.Db
{
    /// <summary>
    /// 底层基础处理仓储
    /// </summary>
    public abstract class BaseRepository<TEntity> : IDisposable where TEntity : class, new()
    {

        protected string TableName = typeof(TEntity).Name;

        /// <summary>
        /// IDbConnection
        /// </summary>
        private readonly IDbConnection _connection;
        

        /// <summary>
        /// IDbCommand
        /// </summary>
        private readonly IDbCommand _cmd;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        protected BaseRepository(string name)
        {
            if (_connection == null)
            {
                _connection = ConnectionFactory.GetConnection(name);
            }
            _cmd = _connection.CreateCommand();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="unit">工作单元</param>
        protected BaseRepository(IUnitOfWork unit)
        {
            if (unit == null)
            {
                return;
            }
            _cmd = unit.Command;
        }


        #region AddParameter


        /// <summary>
        /// AddParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        protected void AddParameter(string name, object value, ParameterDirection direction = ParameterDirection.Input, int size = 0, byte scale = 0)
        {
            IDbDataParameter param = CreateParameter(name, value, direction, size, scale);
            _cmd.Parameters.Add(param);
        }


        /// <summary>
        /// AddParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        protected void AddParameter(string name, object value, DbType type, ParameterDirection direction = ParameterDirection.Input,
            int size = 0, byte scale = 0)
        {
            IDbDataParameter param = CreateParameter(name, value, type, direction, size, scale);
            _cmd.Parameters.Add(param);
        }


        /// <summary>
        /// CreateParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private IDbDataParameter CreateParameter(string name, object value, ParameterDirection direction, int size, byte scale)
        {
            IDbDataParameter param = _cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? string.Empty;
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            return param;
        }


        /// <summary>
        /// CreateParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private IDbDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direction, int size, byte scale)
        {
            IDbDataParameter param = CreateParameter(name, value, direction, size, scale);
            param.DbType = type;
            return param;
        }


        /// <summary>
        /// 清除参数
        /// </summary>
        protected void ClearParameters()
        {
            _cmd.Parameters.Clear();
        }

        #endregion


        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="behavior"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected IDataReader ExecuteReader(string sql, CommandType type = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, int timeout = 30)
        {
            _cmd.CommandText = sql;
            _cmd.CommandType = type;
            _cmd.CommandTimeout = timeout;
            return _cmd.ExecuteReader(behavior);
        }

        /// <summary>
        /// ExecuteTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="behavior"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected DataTable ExecuteTable(string sql, CommandType type = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, int timeout = 30)
        {
            using (IDataReader dr = ExecuteReader(sql, type, behavior, timeout))
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }


        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="behavior"></param>
        /// <param name="timeout"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected DataSet ExecuteDataSet(string sql, CommandType type = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, int timeout = 30, params string[] tableName)
        {
            using (IDataReader dr = ExecuteReader(sql, type, behavior, timeout))
            {
                DataSet ds = new DataSet();
                ds.Load(dr, LoadOption.Upsert, tableName);
                return ds;
            }
        }


        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="type">type</param>
        /// <param name="timeout">timeout</param>
        /// <returns>result</returns>
        protected object ExecuteScalar(string sql, CommandType type = CommandType.Text, int timeout = 30)
        {
            _cmd.CommandText = sql;
            _cmd.CommandType = type;
            _cmd.CommandTimeout = timeout;
            object result = _cmd.ExecuteScalar();
            return result == DBNull.Value ? null : result;
        }


        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string sql, CommandType type = CommandType.Text, int timeout = 30)
        {
            _cmd.CommandText = sql;
            _cmd.CommandType = type;
            _cmd.CommandTimeout = timeout;
            return _cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _cmd?.Dispose();
            _connection?.Dispose();
        }
    }
}