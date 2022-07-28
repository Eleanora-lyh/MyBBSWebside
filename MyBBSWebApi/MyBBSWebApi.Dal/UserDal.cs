using MyBBSWebApi.Dal.Core;
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
        public List<User> GetAll()
        {
            DataTable dataTable = MySqlHelper1.ExcuteTable("select * from user");
            List<User> userList = ToModelList(dataTable);
            return userList;
        }
        public User GetUserById(int id)
        {
            DataRow row = null;
            DataTable dataTable = MySqlHelper1.ExcuteTable("select * from user where id = @id",
                new MySqlParameter("@id", id));
            if (dataTable.Rows.Count > 0) row = dataTable.Rows[0];
            User newUser = ToModel(row);
            return newUser;
        }
        public List<User> GetUserByNoAndPwd(string userno, string password)
        {
            DataTable res = MySqlHelper1.ExcuteTable("select * from user where userno = @userno and password = @password",
                new MySqlParameter("@userno", userno), new MySqlParameter("@password", password)
                );
            List<User> userList = ToModelList(res);
            return userList;
        }
        public int AddUser(User user)
        {
            return MySqlHelper1.ExcuteNoneQuery("insert into user(userno,user_name,user_level,password,is_delete)" +
                " values(@userno,@userName,@userLevel,@password,@is_delete);",
                new MySqlParameter("@userno", user.UserNo),
                new MySqlParameter("@userName", user.UserName),
                new MySqlParameter("@userLevel", user.UserLevel),
                new MySqlParameter("@password", user.Password),
                new MySqlParameter("@is_delete", user.IsDelete)
                );
        }
        public int UpdateUser(int id, string userNo, string? userName, string password, int? userLevel)
        {
            DataTable res = MySqlHelper1.ExcuteTable("SELECT * FROM user Where id = @Id", new MySqlParameter("@Id", id));
            int rowCount = 0;
            if (res.Rows.Count > 0)
            {
                DataRow row = res.Rows[0];
                User user = new User();
                user.Id = (int)row["id"];
                user.UserNo = userNo ?? row["userno"].ToString();
                user.UserName = userName ?? row["user_name"].ToString();
                user.UserLevel = userLevel ?? (int)row["user_level"];
                user.Password = password ?? row["password"].ToString();
               /* user.Token = token ?? new Guid();
                user.AutoLoginTag = autoLoginTag ?? new Guid();
                user.AutoLoginLimitTime = autoLoginLimitTime;*/
                rowCount = MySqlHelper1.ExcuteNoneQuery(
                "update user set userno = @UserNo,user_name = @UserName,user_level=@UserLevel, password = @Password WHERE id = @Id",
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
        private User ToModel(DataRow row)
        {
            User user = new();
            user.Id = (int)MySqlHelper1.FromDbValue(row["id"]);
            user.UserNo = MySqlHelper1.FromDbValue(row["userno"]).ToString();
            user.UserName = MySqlHelper1.FromDbValue(row["user_name"]).ToString();
            user.UserLevel = (int)MySqlHelper1.FromDbValue(row["user_level"]);
            user.Password = MySqlHelper1.FromDbValue(row["password"]).ToString();
            //user.IsDelete = (bool)row["is_delete"];
            /*     user.Token = (Guid?)MySqlHelper1.FromDbValue(row["Token"]);
                 user.AutoLoginTag = (Guid?)MySqlHelper1.FromDbValue(row["AutoLoginTag"]);
                 user.AutoLoginLimitTime = (DateTime?)MySqlHelper1.FromDbValue(row["AutoLoginLimitTime"]);*/
            return user;
        }
        private List<User> ToModelList(DataTable dataTable)
        {
            List<User> userList = new();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                userList.Add(ToModel(row));
            }
            return userList;
        }
    }
}
