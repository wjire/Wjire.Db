﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Wjire.Dapper.Connection;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Dapper
{
    /// <summary>
    /// 底层基础处理仓储
    /// </summary>
    public abstract class BaseRepository<TEntity> : IDisposable where TEntity : class, new()
    {
        protected readonly string TableName = typeof(TEntity).Name;
        private static readonly IConnectionFactoryProvider ConnectionFactoryProvider = ServiceCollectionExtensions.GetRequiredService<IConnectionFactoryProvider>();
        private readonly string _connectionString;
        private readonly IUnitOfWork _unit;
        private IDbConnection _connection;
        private IDbConnection Connection =>
            _unit == null
                ? (_connection ?? (_connection = ConnectionFactoryProvider.ConnectionFactory(_connectionString)))
                : (_unit.Connection ?? (_unit.Connection = ConnectionFactoryProvider.ConnectionFactory(_unit.ConnectionString)));

        private IDbTransaction Transaction =>
            (_unit?.TransactionFactory == null)
            ? null
            : (_unit.Transaction = _unit.Transaction ?? _unit.TransactionFactory(Connection));


        protected BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        protected BaseRepository(IUnitOfWork unit)
        {
            _unit = unit;
        }


        protected int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Execute(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, Transaction, commandTimeout, commandType);
        }

        protected TEntity ExecuteScalar(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalar<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalar<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<TEntity> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalarAsync<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalarAsync<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteReader(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteReaderAsync(sql, param, Transaction, commandTimeout, commandType);
        }

        protected IEnumerable<TEntity> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Query<TEntity>(sql, param, Transaction, buffered, commandTimeout, commandType);
        }

        protected IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Query<T>(sql, param, Transaction, buffered, commandTimeout, commandType);
        }

        protected Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryAsync<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryAsync<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected TEntity QueryFirst(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirst<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected T QueryFirst<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirst<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<TEntity> QueryFirstAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstAsync<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<T> QueryFirstAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstAsync<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected TEntity QueryFirstOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstOrDefault<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected T QueryFirstOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstOrDefault<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<TEntity> QueryFirstOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstOrDefaultAsync<T>(sql, param, Transaction, commandTimeout, commandType);
        }


        protected TEntity QuerySingle(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingle<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected T QuerySingle<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingle<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<TEntity> QuerySingleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingleAsync<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingleAsync<T>(sql, param, Transaction, commandTimeout, commandType);
        }


        protected TEntity QuerySingleOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingleOrDefault<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected T QuerySingleOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingleOrDefault<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<TEntity> QuerySingleOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingleOrDefaultAsync<TEntity>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QuerySingleOrDefaultAsync<T>(sql, param, Transaction, commandTimeout, commandType);
        }

        protected SqlMapper.GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryMultiple(sql, param, Transaction, commandTimeout, commandType);
        }

        protected Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryMultipleAsync(sql, param, Transaction, commandTimeout, commandType);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}