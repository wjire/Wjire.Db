using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Wjire.Db.Dapper.SqlServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly CompanyLogic _logic;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public TestController(CompanyLogic logic, IWebHostEnvironment env,IConfiguration configuration)
        {
            _logic = logic;
            _env = env;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Add()
        {
            Company company = new Company
            {
                CompanyName = "test",
            };
            _logic.Add(company);
            return Ok(_configuration["configName"]);
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_logic.Get());
        }


        [HttpGet]
        public string Update()
        {
            _logic.AddOrUpdate(new Company
            {
                CompanyName = "name111",
                Id = 60
            });
            return "1";
        }


        [HttpGet]
        public long AddAndReturnIdentity()
        {
            return _logic.AddAndReturnIdentity(new Company
            {
                CompanyName = "name111",
            });
        }
    }
}