using MyBBSWebApi.Bll;
using MyBBSWebApi.Dal;
using MyBBSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Bll
{
    public class UserBll : IUserBll
    {
        public List<User> GetAll()
        {
            UserDal userDal = new UserDal();
            //查询时只取没有被删除的记录
            List<User> userList = userDal.GetAll().FindAll(m=>!m.IsDelete);
            return userList;
        }
        public User CheckLogin(string userno,string password)
        {
            UserDal userDal = new UserDal();
            List<User> userlist = userDal.GetUserByNoAndPwd(userno, password);
            if (userlist.Count <= 0)
            {
                return null;
            }
            else
            {
                //如果isDelete属性为false，返回UserList
                //如果isDelete属性为true，则被删除了，返回null
                return userlist.Find(m => !m.IsDelete);
            }
        }
        public string AddUser(User user)
        {
            user.IsDelete = false;
            //user.Password = user.Password.ToMd5();
            UserDal userDal = new UserDal();
            int rows = userDal.AddUser(user);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
        public string UpdateUser(
           int id,
           string userNo,
           string? userName,
           string password,
           int? userLevel)
          /* Guid? token,
           Guid? autoLoginTag,
           DateTime? autoLoginLimitTime)*/
        {
            UserDal userDal = new UserDal();
            int rows = userDal.UpdateUser(id, userNo, userName, password, userLevel/*, token, autoLoginTag, autoLoginLimitTime*/);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }

        public string RemoveUser(int id)
        {

            UserDal userDal = new UserDal();
            int rows = userDal.RemoveUser(id);
            if (rows > 0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
    }
}
