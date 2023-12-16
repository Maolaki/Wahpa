using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEngine
{
    internal static class DataBase
    {
        public static void DBGetRowsCount(out int rows)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", conn))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                conn.Close();
            }

            rows = dt.Rows.Count;
        }

        public static void DBAddUser(string login, string password, int mageClass)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text; 
                    cmd.CommandText = "INSERT INTO users (login, password, coins, access, mageClass) VALUES (@login, @password, @coins, @access, @mageClass)"; 

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password); 
                    cmd.Parameters.AddWithValue("@coins", 0); 
                    cmd.Parameters.AddWithValue("@access", true);
                    cmd.Parameters.AddWithValue("@mageClass", mageClass);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void DBDeleteUser(string login)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM users WHERE login = @login";

                    cmd.Parameters.AddWithValue("@login", login);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static bool DBCheckUser(string login)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE login = @login";

                    cmd.Parameters.AddWithValue("@login", login);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return count == 0;
                }
            }
        }

        public static void DBBlock(string login, bool access)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE users SET access = @access WHERE login = @login";

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@access", access);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static bool DBVerifyUser(string login, string password)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE login = @login AND password = @password";

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();

                    return count > 0; 
                }
            }
        }
    }
}
