using DotNetCore.CAP;

namespace Wjire.Dapper.SqlServer.CAP
{
    public static class DbContextExtension
    {
        public static IUnitOfWork CreateCapTransaction(this DbContext dbContext, ICapPublisher cap, bool autoCommit = false)
        {
            return new CapTransaction(dbContext.Write, cap, autoCommit);
        }
    }
}