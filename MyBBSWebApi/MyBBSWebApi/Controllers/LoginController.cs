using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBBSWebApi.Bll;
using MyBBSWebApi.Dal.Core;
using MyBBSWebApi.Dal;
using MyBBSWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace MyBBSWebApi.Controllers
{
    [Route("[controller]/[action]")]
    //[Route("[controller]")]
    [ApiController]
    //[EnableCors("any")]
    /*
     * 通过具体的http请求的方式来获取的风格叫做restful
     * action能获取到具体的方法
     */
    public class LoginController : ControllerBase
    {
        private readonly IUserBll userBll;

        public LoginController(IUserBll userBll)
        {
            this.userBll = userBll;
        }
        [HttpGet]
        /*
         * 查询所有用户信息
        */
        public List<User> GetAll()
        {
            return userBll.GetAll();
        }
        [HttpGet]
        /*
         * 根据userno、password查询
        */
        public User CheckLogin(string userno, string password)
        {
            return userBll.CheckLogin(userno, password); ;
        }
        [HttpPost]
        public string Insert(User user)
        {
            return userBll.AddUser(user);
        }
        [HttpPut]
        public string Update(int id, string userNo, string? userName, string password, int? userLevel)
        {
            return userBll.UpdateUser(id, userNo, userName, password, userLevel);
        }
        [HttpDelete]
        public string Remove(int id)
        {
            return userBll.RemoveUser(id);
        }
    }
}
