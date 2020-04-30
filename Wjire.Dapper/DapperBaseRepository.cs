using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Wjire.Dapper
{
    /// <summary>
    /// 底层基础处理仓储
    /// </summary>
    public abstract class DapperBaseRepository<TEntity> : IDisposable where TEntity : class, new()
    {
        private readonly string _connectionString;
        private readonly IUnitOfWork _unit;
        private IDbConnection _connection;
        protected IDbConnection Connection =>
            _unit == null
                ? _connection ?? (_connection = ConnectionHelper.ConnectionFactory(_connectionString))
                : _unit.Connection ?? (_unit.Connection = ConnectionHelper.ConnectionFactory(_unit.ConnectionString));

        protected IDbTransaction Transaction =>
            _unit?.TransactionFactory == null
            ? null
            : _unit.Transaction ?? (_unit.Transaction = _unit.TransactionFactory(Connection));


        protected DapperBaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        protected DapperBaseRepository(IUnitOfWork unit)
        {
            _unit = unit;
        }


        #region 常用方法

        /// <summary>
        /// 添加单条
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void Insert(TEntity entity);
    

        /// <summary>
        /// 添加单条记录,并返回新增记录的自增键的值.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>新增数据的主键</returns>
        protected abstract long InsertAndReturnIdentity(TEntity entity);


        /// <summary>
        /// 存在更新,不存在插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        /// <param name="updateField">如果存在,需要更新的字段</param>
        protected abstract void InsertOrUpdate(TEntity entity, object whereField, object updateField);


        /// <summary>
        /// 如果不存在则插入,否则忽略
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereField">判断是否存在的匹配字段</param>
        protected abstract void InsertIfNotExists(TEntity entity, object whereField);


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
        protected IEnumerable<TEntity> Page(string dataSql, string countSql, int pageIndex,
            int pageSize, out int pageCount, out int dataCount, object param = null)
        {
            return Page<TEntity>(dataSql, countSql, pageIndex, pageSize, out pageCount, out dataCount, param);
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
        protected abstract IEnumerable<T> Page<T>(string dataSql, string countSql, int pageIndex,
            int pageSize, out int pageCount, out int dataCount, object param = null);

        #endregion


        #region Dapper

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

        #endregion


        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}