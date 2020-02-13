using System.Data;

namespace Wjire.Db.Dapper
{
    /// <summary>
    /// 事务操作
    /// </summary>	
    public class TransactionConnection : IUnitOfWork
    {

        public AbstractConnectionWrapper ConnectionWrapper { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <param name="isolationLevel">事务级别</param>
        public TransactionConnection(string name, IsolationLevel? isolationLevel = null)
        {
            ConnectionWrapper = ConnectionWrapperFactory.GetConnectionWrapper(name);
            ConnectionWrapper.Transaction = isolationLevel.HasValue
                ? ConnectionWrapper.Connection.BeginTransaction(isolationLevel.Value)
                : ConnectionWrapper.Connection.BeginTransaction();
        }



        /// <summary>
        /// 提交事物
        /// </summary>
        public void Commit()
        {
            ConnectionWrapper.Transaction.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
            ConnectionWrapper.Transaction.Rollback();
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            ConnectionWrapper.Connection?.Dispose();
        }
    }
}
