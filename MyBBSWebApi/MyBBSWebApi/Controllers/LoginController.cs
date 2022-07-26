using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        /*
         * 获取conn对象
         */
        public MySqlConnection GetConn()
        {
            //与数据库连接的信息可以使用 MySqlConnectionStringBuilder类简化字符串的拼接
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.UserID = "root";
            builder.Password = "root";
            builder.Server = "192.168.0.120";
            builder.Database = "mybbsdb";
            builder.Port = 3307;
            MySqlConnection conn = new MySqlConnection(builder.ConnectionString);
            Console.WriteLine("数据库连接成功");
            return conn;
        }
        [HttpGet]
        /*
         * 获取conn对象
        */
        public string Get(string userNo, string password)
        {
            MySqlConnection conn = GetConn();
            MySqlCommand cmd = new MySqlCommand("select * from user",conn);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            mySqlDataAdapter.Fill(ds);
            var res = ds.Tables[0];
                
            return res.Rows[0]["user_name"].ToString();
        }
        [HttpPost]
        public string Insert()
        {
            return "lyhhh";
        }
        [HttpPut]
        public string Update()
        {
            return "lyhhh";
        }
        [HttpDelete]
        public string Remove()
        {
            return "lyhhh";
        }
    }
}
