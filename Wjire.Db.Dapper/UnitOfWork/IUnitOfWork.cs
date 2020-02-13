using System;

namespace Wjire.Db.Dapper
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        AbstractConnectionWrapper ConnectionWrapper { get; }

        void Commit();

        void Rollback();
    }
}
