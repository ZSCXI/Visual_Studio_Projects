using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PhotonServer
{
    class Program
    {
        //查询操作
        static void Query(MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            string sql = @"select * from users"; //查询

            //cmd.ExecuteReader();//执行查询
            //cmd.ExecuteScalar(); //执行查询，返回单个值
            //cmd.ExecuteNonQuery(); //插入 删除

            MySqlCommand cmd = new MySqlCommand(sql, conn);//查询
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
                //如果该列为Null值，不能采用Get形式
                Console.WriteLine(reader.GetInt32("id") + " " + reader.GetString("username") + " " + reader.GetString("password") + " " + reader[3].ToString());
            }
        }

        static void Insert(MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            string sql = @"insert into users(username,password,registerdate) values ('ojbk2','789','"+DateTime.Now+"')";//插入
            MySqlCommand cmd = new MySqlCommand(sql, conn);//插入
            int result = cmd.ExecuteNonQuery(); //返回值是数据库中受影响的记录
            if (result > 0)
            {
                Console.WriteLine("插入了"+result + "条数据.");
            }
        }

        static void Update(MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            string sql = @"update users set username = '7894',password = '45666' where id = 1";//修改
            MySqlCommand cmd = new MySqlCommand(sql, conn);//修改
            int result = cmd.ExecuteNonQuery(); //返回值是数据库中受影响的记录
            if (result > 0)
            {
                Console.WriteLine("修改了" + result + "条数据.");
            }
        }

        static void Delete(MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            string sql = @"delete from users where id = 9";//删除
            MySqlCommand cmd = new MySqlCommand(sql, conn);//删除
            int result = cmd.ExecuteNonQuery(); //返回值是数据库中受影响的记录
            if (result > 0)
            {
                Console.WriteLine("删除了" + result + "条数据.");
            }
        }

        //利用ExecuteReader()函数读取单个数据
        static void ReadUserCount(MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            string sql = @"select count(*) from users"; //查询

            MySqlCommand cmd = new MySqlCommand(sql, conn);//查询

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            int count = Convert.ToInt32(reader[0].ToString());

            Console.WriteLine(count);
        }

        //利用ExecuteScalar()函数读取单个数据
        static void ExecuteScaler(MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            string sql = @"select count(*) from users"; //查询

            MySqlCommand cmd = new MySqlCommand(sql, conn);//查询

            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            Console.WriteLine(count);
        }

        static bool VerifyUser(string username, string password, MySqlConnection conn)
        {
            conn.Close();
            conn.Open();
            //1.按照查询条件组拼SQL语句
            //string sql = @"select * from users where username = '"+username+"' and password = '"+password+"'"; //查询

            //2.按照@符号进行参数表示
            string sql_str = @"select * from users where username = @username1 and password = @password1"; //查询

            MySqlCommand cmd = new MySqlCommand(sql_str, conn);//查询

            //按照方式2时，需要在CMD中传入实际参数
            cmd.Parameters.AddWithValue("username1",username);
            cmd.Parameters.AddWithValue("password1",password);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            string connectStr = "server=127.0.0.1;port = 3306; database = mygamedb;user=root;password = root;";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                //Query(conn);
                //Update(conn);
                //Delete(conn);
                //ReadUserCount(conn);
                //ExecuteScaler(conn);
                Console.WriteLine(VerifyUser("lyl123","123",conn));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            Console.ReadKey();
        }
    }
}
