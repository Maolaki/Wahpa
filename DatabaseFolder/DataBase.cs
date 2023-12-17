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

        public static User logUser {  get; set; }
        public static int userLen {  get; set; }
        public static List<User> users {  get; set; }

        public static int page {  get; set; }

        public static void DBInitUsers()
        {
            users = new List<User>();

            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    // Изменяем запрос так, чтобы сортировать пользователей по убыванию количества монет
                    cmd.CommandText = "SELECT login, password, coins, access, mageClass FROM users ORDER BY coins DESC";

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.login = reader.GetString(0);
                            user.password = reader.GetString(1);
                            user.coins = reader.GetInt32(2);
                            user.access = reader.GetBoolean(3);
                            user.mageClass = reader.GetInt32(4);

                            users.Add(user);
                        }
                    }
                }

                conn.Close();
            }
        }

        public static void DBGetRowsCount()
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

            userLen = dt.Rows.Count;
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

        public static bool DBCheckLogin(string login)
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

        public static void DBBlock(string login)
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
                    cmd.Parameters.AddWithValue("@access", false);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static bool DBLogUser(string login, string password)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM users WHERE login = @login AND password = @password";

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && reader.GetBoolean(3) == true)
                        {
                            logUser = new User();
                            logUser.login = reader.GetString(0);
                            logUser.password = reader.GetString(1);
                            logUser.coins = reader.GetInt32(2);
                            logUser.access = reader.GetBoolean(3);
                            logUser.mageClass = reader.GetInt32(4);

                            conn.Close();

                            return true; 
                        }
                        else
                        {
                            conn.Close();

                            return false; 
                        }
                    }
                }
            }
        }

        public static void DBAddCoins(int coins)
        {
            string constr = "Server=localhost;Database=Wahpa;Uid=root;Pwd=1111;";

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE users SET coins = coins + @coins WHERE login = @login";

                    cmd.Parameters.AddWithValue("@login", logUser.login);
                    cmd.Parameters.AddWithValue("@coins", coins);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

    }
}
