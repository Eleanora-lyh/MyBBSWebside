using MyBBSWebApi.Core;
using MyBBSWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Dal
{
    public class UserDal
    {
        public User GetUserById(int id)
        {
            DataRow row = null;
            DataTable dataTable = MySqlHelper1.ExcuteTable("select * from user where id = @id",
                new MySqlParameter("@id", id));
            if (dataTable.Rows.Count > 0) row = dataTable.Rows[0];
            User newUser = new User();
            newUser.Id = (int)row["id"];
            newUser.UserName = row["user_name"].ToString();
            newUser.Password = row["password"].ToString();
            newUser.UserLevel = (int)row["user_level"];
            newUser.UserNo = row["user_no"].ToString();

            return newUser;
        }
        public bool GetUserByNoAndPwd(string userno, string password)
        {
            DataRow row = null;
            DataTable dataTable = MySqlHelper1.ExcuteTable("select * from user where userno = @userno and password = @password",
                new MySqlParameter("@userno", userno), new MySqlParameter("@password", password)
                );
            if (dataTable.Rows.Count > 0) return true;
            else return false;
        }
        public int AddUser(string userno, string userName, int userLevel, string password)
        {
            return MySqlHelper1.ExcuteNoneQuery("insert into user(userno,user_name,user_level,password)" +
                " values(@userno,@userName,@userLevel,@password);",
                new MySqlParameter("@userno", userno),
                new MySqlParameter("@userName", userName),
                new MySqlParameter("@userLevel", userLevel),
                new MySqlParameter("@password", password)
                );
        }
        public int UpdateUser(int id, string userNo, string userName, string password, int? userLevel)
        {
            DataTable res = MySqlHelper1.ExcuteTable("SELECT * FROM user Where id = @Id", new MySqlParameter("@Id", id));
            int rowCount = 0;
            if (res.Rows.Count > 0)
            {
                DataRow row = res.Rows[0];
                User user = new User();
                user.Id = (int)row["Id"];
                user.UserNo = userNo ?? row["UserNo"].ToString();
                user.UserName = userName ?? row["UserName"].ToString();
                user.UserLevel = userLevel ?? (int)row["UserLevel"];
                user.Password = password ?? row["Password"].ToString();
                MySqlHelper1.ExcuteNoneQuery(
                "update user set userno = @UserNo,user_name = @UserName,user_level=@UserLevel, Password = @Password WHERE Id = @Id",
                 new MySqlParameter("@UserNo", user.UserNo),
                 new MySqlParameter("@UserName", user.UserName),
                 new MySqlParameter("@UserLevel", user.UserLevel),
                 new MySqlParameter("@Password", user.Password),
                 new MySqlParameter("@Id", user.Id)
                );
            }
            return rowCount;
        }
        public int RemoveUser(int id)
        {
            return MySqlHelper1.ExcuteNoneQuery("delete from user where id = @id",
                new MySqlParameter("@id", id)
                );
        }
    }
}
