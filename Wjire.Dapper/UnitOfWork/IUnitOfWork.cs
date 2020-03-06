using System;
using System.Data;

namespace Wjire.Dapper
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }
        string ConnectionString { get; set; }
        Func<IDbConnection, IDbTransaction> TransactionFactory { get; }
        void Commit();
        void Rollback();
    }
}
