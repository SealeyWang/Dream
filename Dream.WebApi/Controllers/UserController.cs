using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Dream.Service;
using Dream.Entity;
using static System.Net.WebRequestMethods;
using System.Net;
using Nest;

namespace Dream.WebApi.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private UserService userService;

        public UserController()
        {
            userService = new UserService();
        }
        // http://localhost:5000/api/WeatherForecast2/2
        [HttpPost]
        [Route("login")]
        public String Get(String userName, String password)
        {
            Console.WriteLine($"Info: {userName}");
            Console.WriteLine($"Info2: {password}");

            // TODO get user 
            User u =  userService.queryUser(userName, password);
            BaseResult<User> result = new BaseResult<User>(0, "ok", u);
            String json = result.ToJson();
            return json;

        }

       [HttpGet]
       [Route("list")]
       public String users()
        {
            
            return userService.listUser();
        }

        [HttpPost]
        [Route("register")]
        public String Register([FromBody] User data)
        {
            Console.WriteLine("Register");
            Console.WriteLine(data);
            if (data.User_Name == null) return "必须输入用户名";
            if (data.User_Password == null) return "必须输入密码";
            if (data.User_Email == null) return "必须输入邮箱";



            //u.User_Name = "wsl";
            //u.User_Email = "wangshaoli18871074717@gmail.com";
            //u.User_Password = "123456";
            BaseResult<String> result = new BaseResult<String>();
            try
            {
                String id = userService.RegisterUser(data);
                result.code = 0;
                result.msg = "ok";
                result.data = id;
            }
            catch (Exception e )
            {
                result.code = 500;
                result.msg = e.ToString();
            }
          
            return result.ToJson();
        }

        [HttpPost] 
        [Route("resetPassword")]
        public String ResetPassword(String uid, String newPassword)
        {
            String data = userService.ResetPassword(uid, newPassword);

            BaseResult<String> result = new BaseResult<String>(0, "ok", data);

            return result.ToJson();
        }

    }

}
