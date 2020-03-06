using System.Collections.Generic;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Wjire.Dapper;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{
    public class CompanyLogic
    {
        private readonly ICapPublisher _cap;
        private readonly DbContext _dbContext;
        private readonly ILogger<CompanyLogic> _logger;

        public CompanyLogic(ICapPublisher cap, DbContext dbContext, ILogger<CompanyLogic> logger)
        {
            _cap = cap;
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Add(Company company)
        {
            using (CompanyRepository db = new CompanyRepository(_dbContext.Write))
            {
                db.Add(company);
            }
        }

        public long AddAndReturnIdentity(Company company)
        {
            using (CompanyRepository db = new CompanyRepository(_dbContext.Write))
            {
                return db.AddAndReturnIdentity(company);
            }
        }

        public void AddOrUpdate(Company company)
        {
            using (CompanyRepository db = new CompanyRepository(_dbContext.Write))
            {
                db.AddOrUpdate(company);
            }
        }

        public IEnumerable<Company> QueryPage(int pageIndex, int pageSize)
        {
            using (CompanyRepository db = new CompanyRepository(_dbContext.Read))
            {
                return db.QueryPage(pageIndex, pageSize, out int pageCount, out int dataCount);
            }
        }

        public IEnumerable<Company> Get()
        {
            using (CompanyRepository db = new CompanyRepository(_dbContext.Read))
            {
                return db.Page();
            }
        }
    }
}
