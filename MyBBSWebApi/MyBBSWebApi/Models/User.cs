using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public int UserLevel { get; set; }
        public string Password{ get; set; }
        public bool IsDelete { get; set; }
    }
}
