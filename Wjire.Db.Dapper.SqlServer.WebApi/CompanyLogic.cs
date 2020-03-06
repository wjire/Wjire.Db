using System;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Wjire.Dapper;
using Wjire.Dapper.SqlServer.CAP;

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
            using (IUnitOfWork unit = _dbContext.CreateCapTransaction(_cap))
            {
                try
                {
                    CompanyRepository db = new CompanyRepository(unit);
                    db.Add(company);
                    _cap.Publish("test.company.add", DateTime.Now);
                    unit.Commit();
                }
                catch (Exception ex)
                {
                    unit.Rollback();
                    _logger.LogError(ex.ToString());
                }
            }
        }


        public string Get()
        {
            using (CompanyRepository db = new CompanyRepository(_dbContext.Read))
            {
                return db.Get(1);
            }
        }
    }
}
