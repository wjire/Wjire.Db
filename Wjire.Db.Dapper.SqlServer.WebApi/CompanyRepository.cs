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
            Connection.Add(company, Transaction);
        }

        public long AddAndReturnIdentity(Company company)
        {
            return Connection.AddAndReturnIdentity(company, Transaction);
        }

        public void AddOrUpdate(Company company)
        {
            //TODO
            Connection.AddOrUpdate(company, new { company.Id }, Transaction);
        }

        public IEnumerable<Company> QueryPage(int pageIndex, int pageSize, out int pageCount, out int dataCount)
        {
            //TODO
            string dataSql = "select * from Company order by id";
            string countSql = "select count(0) from Company";
            return Connection.QueryPage<Company>(dataSql, countSql, pageIndex, pageSize, out pageCount, out dataCount, Transaction);
        }


        public string Get(int id)
        {
            return ExecuteScalar<string>("select companyName from Company where id = " + id);
        }

        public IEnumerable<Company> Page()
        {
            string dataSql = "select * from Company order by id";
            string countSql = "select count(0) from Company";
            return Connection.QueryPage<Company>(dataSql, countSql, 2, 10, out int pageCount, out int dataCount, Transaction);
        }
    }
}