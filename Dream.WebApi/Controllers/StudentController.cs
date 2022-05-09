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
    public class StudentController : ControllerBase
    {
        private StudentService studentService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public StudentController()
        {
            studentService = new StudentService();
        }

        [HttpPost]
        [Route("add")]
        public String AddStudent(Student s)
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = studentService.AddStudent(s);
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
        public String listStudents()
        {
            String result;
            BaseResult<String> r;
            try
            {
                result = studentService.listStudent();
                r = new BaseResult<String>(0, "ok", result);

            }
            catch (Exception e)
            {
                result = e.ToString();
                r = new BaseResult<String>(500, "server error", result);

            }
            return r.ToJson();
        }

        [HttpGet]
        [Route("remove")]
        public String removeStudent(String uname)
        {
            Console.WriteLine($"uname = {uname}");
            String result;
            BaseResult<String> r;
            try
            {
                result = studentService.removeStudent(uname);
                r = new BaseResult<String>(0, "ok", result);

            }
            catch (Exception e)
            {
                result = e.ToString();
                r = new BaseResult<String>(500, "server error", result);

            }
            return r.ToJson();
        }


        [HttpGet]
        [Route("query")]
        public String queryStudent(String uname)
        {
            Console.WriteLine($"uname = {uname}");
            String result;
            BaseResult<String> r;
            try
            {
                result = studentService.queryStudent(uname);
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
