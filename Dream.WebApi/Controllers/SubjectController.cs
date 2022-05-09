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
    public class SubjectController : ControllerBase
    {
        private SubjectService subjectService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public SubjectController()
        {
        
            subjectService = new SubjectService();
        }

        [HttpPost]
        [Route("add")]
        public String AddSubject(Subject s)
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = subjectService.AddSubject(s);
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
        public String listSubject()
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = subjectService.listSubject();
                r = new BaseResult<String>(0, "ok", result);

            }
            catch (Exception e)
            {
                result = e.ToString();
                r = new BaseResult<String>(550, "server error", result);

            }
            return r.ToJson();
        }

    }

}
