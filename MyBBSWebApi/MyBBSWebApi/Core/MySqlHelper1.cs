using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Core
{
    public static class MySqlHelper1 { 
    
        public static string ConnStr { get; set; } = "server=192.168.0.120;port=3307;user=root;password=root; database=mybbsdb; ";
        //params 关键字可以使得所填参数为任意个，但其所修饰的变量必须放在最后
        public static DataTable ExcuteTable(string sqlStr, params MySqlParameter[] sqlPram)
        {
            //在获取连接的时候使用using关键字可以自动释放conn连接
            using MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
            cmd.Parameters.AddRange(sqlPram);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data.Tables[0];
        }
        public static int ExcuteNoneQuery(string sqlStr, params MySqlParameter[] sqlPram)
        {
            using MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlStr,conn);
            cmd.Parameters.AddRange(sqlPram);
            return  cmd.ExecuteNonQuery();
        }
        public static object ToDbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }
        public static object FromDbValue(object value)
        {
            return value == DBNull.Value ? null : value;
        }
    }
}
