﻿using MyBBSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Bll
{
    /*
     * 此接口用于规范User相关的方法名称以及返回值
     */
    public interface IUserBll
    {
        public List<User> GetAll();
        public User CheckLogin(string userno, string password);
        public string AddUser(User user);
        public string UpdateUser(int id, string userNo, string? userName, string password, int? userLevel);
        public string RemoveUser(int id);

    }
}
