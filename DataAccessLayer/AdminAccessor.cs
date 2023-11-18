using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class AdminAccessor : AdminAccessorInterface
    {
        public int insertUser(User user)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_insert_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return result;
        }

        public List<string> selectRoles()
        {
            List<string > roles = new List<string>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_select_roles", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string role = reader.GetString(0);
                        roles.Add(role);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return roles;
        }

        public List<User> selectUsers()
        {
            List<User> users = new List<User>();
            // 1-connect to database
            SqlConnection conn = SqlConnectionProvider.GetConnection();

            // 2- verify the store procedure
            var cmd = new SqlCommand("sp_select_users", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // 4- connect and execute
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       User user = new User();
                        user.UserId = reader.GetInt32(0);
                        user.UserName = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Role = reader.GetString(3);
                        users.Add(user);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return users;
        }

        public int updateUser(User user)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_update_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return result;
        }
    }
}
