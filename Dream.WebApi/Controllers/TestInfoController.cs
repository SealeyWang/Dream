using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dream.Entity;
using Dream.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dream.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestInfoController : ControllerBase
    {
        private TestInfoService newsService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public TestInfoController()
        {
            newsService = new TestInfoService();
        }

        [HttpPost]
        [Route("add")]
        public String AddNews(TestInfo s)
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = newsService.AddTestInfo(s);
                r = new BaseResult<String>(0, "ok", result);

            }
            catch (Exception e)
            {
                result = e.ToString();
                r = new BaseResult<String>(550, "server error", result);

            }
            return r.ToJson();
        }

        [HttpGet]
        [Route("list")]
        public String listTestInfo()
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = newsService.listTestInfo();
                r = new BaseResult<String>(0, "ok", result);

            }
            catch (Exception e)
            {
                result = e.ToString();
                r = new BaseResult<String>(500, "server error", result);

            }
            return r.ToJson();
        }

    }

}
