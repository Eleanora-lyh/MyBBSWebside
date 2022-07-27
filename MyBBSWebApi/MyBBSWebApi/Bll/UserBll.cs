using MyBBSWebApi.Bll.Interfaces;
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
        UserDal userDal = new();
        public List<User> GetAll()
        {
            //查询时只取没有被删除的记录
            List<User> userList = userDal.GetAll().FindAll(m=>!m.IsDelete);
            return userList;
        }
            public User CheckLogin(string userno,string password)
        {
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
    }
}
