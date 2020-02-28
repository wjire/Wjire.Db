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
    }
}