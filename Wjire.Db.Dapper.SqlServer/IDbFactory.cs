namespace Wjire.Db.Dapper.SqlServer
{
    public interface IDbFactory
    {
        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateTransaction();



        /// <summary>
        /// 创建单连接
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateSingle();

    }
}
