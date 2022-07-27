using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBBSWebApi.Bll;
using MyBBSWebApi.Bll.Interfaces;
using MyBBSWebApi.Core;
using MyBBSWebApi.Dal;
using MyBBSWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
{
    [Route("[controller]/[action]")]
    /*
     * 通过具体的http请求的方式来获取的风格叫做restful
     * action能获取到具体的方法
     */
    [ApiController]
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
            return userBll().GetAll();
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
        public int Insert(string userno, string userName, int userLevel, string password)
        {
            UserDal userDal = new UserDal();
            return userDal.AddUser(userno, userName, userLevel, password);
        }
        [HttpPut]
        public string Update(int id, string userNo, string userName, string password, int? userLevel)
        {
            UserDal userDal = new UserDal();
            int rows = userDal.UpdateUser(id, userNo, userName, password, userLevel);
            if (rows > 0)
                return "数据修改成功";
            else return "数据修改失败";
        }
        [HttpDelete]
        public int Remove(int id)
        {
            UserDal userDal = new UserDal();
            return userDal.RemoveUser(id);
        }
    }
}
