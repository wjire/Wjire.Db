
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Wjire.Db.Dapper.SqlCreator;

namespace Wjire.Db.Dapper
{
    /// <summary>
    /// 底层基础处理仓储
    /// </summary>
    public abstract partial class BaseRepository<TEntity> : IDisposable where TEntity : class, new()
    {

        protected string TableName = typeof(TEntity).Name;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly AbstractConnectionWrapper _connectionWrapper;
        private Lazy<AbstractSqlCreator> _sqlCreatorFactory;
        private AbstractSqlCreator SqlCreator => _sqlCreatorFactory.Value;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        protected BaseRepository(string name)
        {
            if (_connectionWrapper == null)
            {
                _connectionWrapper = ConnectionWrapperFactory.GetConnectionWrapper(name);
            }
            Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unit">工作单元</param>
        protected BaseRepository(IUnitOfWork unit)
        {
            _connectionWrapper = unit.ConnectionWrapper;
            Init();
        }


        private void Init()
        {
            _connection = _connectionWrapper.Connection;
            _transaction = _connectionWrapper.Transaction;
            _sqlCreatorFactory = _connectionWrapper.SqlCreatorFactory;
        }


        protected int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Execute(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteAsync(sql, param, _transaction, commandTimeout, commandType);
        }

        protected T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteScalar<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteScalarAsync<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteReader(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteReaderAsync(sql, param, _transaction, commandTimeout, commandType);
        }

        protected IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<T>(sql, param, _transaction, buffered, commandTimeout, commandType);
        }

        protected Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryAsync<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected T QueryFirst<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirst<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<T> QueryFirstAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstAsync<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected T QueryFirstOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstOrDefault<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction, commandTimeout, commandType);
        }


        protected T QuerySingle<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingle<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingleAsync<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected T QuerySingleOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingleOrDefault<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingleOrDefaultAsync<T>(sql, param, _transaction, commandTimeout, commandType);
        }

        protected SqlMapper.GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryMultiple(sql, param, _transaction, commandTimeout, commandType);
        }

        protected Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryMultipleAsync(sql, param, _transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}