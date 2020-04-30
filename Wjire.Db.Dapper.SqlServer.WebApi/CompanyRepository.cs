using System.Collections.Generic;
using Wjire.Dapper;
using Wjire.Dapper.SqlServer;

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
            Insert(company);
        }

        public long AddAndReturnIdentity(Company company)
        {
            return InsertAndReturnIdentity(company);
        }

        public void AddOrUpdate(Company company)
        {
            InsertOrUpdate(company, new { company.Id }, new { company.CompanyName });
        }

        public IEnumerable<Company> QueryPage(int pageIndex, int pageSize, out int pageCount, out int dataCount)
        {
            //TODO
            string dataSql = "select * from Company order by id";
            string countSql = "select count(0) from Company";
            return Page(dataSql, countSql, pageIndex, pageSize, out pageCount, out dataCount, Transaction);
        }


        public string Get(int id)
        {
            return ExecuteScalar<string>("select companyName from Company where id = " + id);
        }

        public IEnumerable<Company> Page()
        {
            string dataSql = "select * from Company order by id";
            string countSql = "select count(0) from Company";
            return Page(dataSql, countSql, 2, 10, out int pageCount, out int dataCount, Transaction);
        }
    }
}