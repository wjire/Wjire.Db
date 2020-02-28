using System;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Wjire.Dapper.SqlServer.CAP;
using Wjire.Dapper.UnitOfWork;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{
    public class CompanyLogic
    {
        private readonly ICapPublisher _cap;
        private readonly CapDbContext _dbContext;
        private readonly ILogger<CompanyLogic> _logger;

        public CompanyLogic(ICapPublisher cap, CapDbContext dbContext, ILogger<CompanyLogic> logger)
        {
            _cap = cap;
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Add(Company company)
        {
            using (IUnitOfWork unit = _dbContext.CreateCapTransaction(_cap, false))
            {
                try
                {
                    CompanyRepository db = Factory.CreateICompanyRepositoryWrite(unit);
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
            using (CompanyRepository db = Factory.CreateICompanyRepositoryRead())
            {
                return db.Get(1);
            }
        }
    }
}
