using System;
using System.Data;

namespace Wjire.Db.Dapper.SqlServer
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }
        Func<IDbConnection> ConnectionFactory { get; }
        Func<IDbConnection, IDbTransaction> TransactionFactory { get; }
        void Commit();
        void Rollback();
    }
}
