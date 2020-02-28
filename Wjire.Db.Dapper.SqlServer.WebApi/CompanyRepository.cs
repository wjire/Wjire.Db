using Wjire.Dapper;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{

    /// <summary>
    /// CompanyRepository
    /// </summary>
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(string name) : base(name) { }

        public CompanyRepository(IUnitOfWork unit) : base(unit) { }

        public void Add(Company company)
        {
            string sql = "insert into Company values(@companyName)";
            Execute(sql, company);
        }

        public string Get(int id)
        {
            return ExecuteScalar<string>("select companyName from Company where id = " + id);
        }
    }
}