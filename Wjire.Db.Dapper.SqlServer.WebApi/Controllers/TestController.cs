using Microsoft.AspNetCore.Mvc;

namespace Wjire.Db.Dapper.SqlServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly CompanyLogic _logic;

        public TestController(CompanyLogic logic)
        {
            _logic = logic;
        }


        [HttpGet]
        public IActionResult Add()
        {
            Company company = new Company
            {
                CompanyName = "test",
            };
            _logic.Add(company);
            return Ok("success");
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_logic.Get());
        }


        [HttpGet]
        public string Update()
        {
            _logic.Update(new Company
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