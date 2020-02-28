using Wjire.Dapper.UnitOfWork;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{
    public static partial class Factory
    {

        /// <summary>
        /// 创建 ICompanyRepository 读连接
        /// </summary>
        /// <returns></returns>
        public static CompanyRepository CreateICompanyRepositoryRead()
        {
            return new CompanyRepository(Read);
        }

        /// <summary>
        /// 创建 ICompanyRepository 读连接
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static CompanyRepository CreateICompanyRepositoryRead(IUnitOfWork unit)
        {
            return new CompanyRepository(unit);
        }

        /// <summary>
        /// 创建 ICompanyRepository 写连接
        /// </summary>
        /// <returns></returns>
        public static CompanyRepository CreateICompanyRepositoryWrite()
        {
            return new CompanyRepository(Write);
        }

        /// <summary>
        /// 创建 ICompanyRepository 写连接
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static CompanyRepository CreateICompanyRepositoryWrite(IUnitOfWork unit)
        {
            return new CompanyRepository(unit);
        }
    }
}