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
    }
}
