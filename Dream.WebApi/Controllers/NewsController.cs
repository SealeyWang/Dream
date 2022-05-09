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
    public class NewsController : ControllerBase
    {
        private NewsService newsService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public NewsController()
        {
            newsService = new NewsService();
        }

        [HttpPost]
        [Route("add")]
        public String AddNews(News s)
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = newsService.AddNews(s);
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
        public String listNews()
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = newsService.listNews();
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
