using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public int deleteUser(User? user)
        {
            // result
            int result = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_delete_user";
            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters add with values
            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            // try catch finally
            try
            {
                // open connection
                conn.Open();
                // execute command
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // close connection
                conn.Close();
            }
            // return result
            return result;
        }

        public int insertUser(User user)
        {
            // result
            int result = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_insert_user";
            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters add with values
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            // try catch finally
            try
            {
                // open connection
                conn.Open();
                // execute command
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // close connection
                conn.Close();
            }
            // return result
            return result;
        }
        public List<string> selectRoles()
        {
            // list of strings
            List<string> roles = new List<string>();
            // connection
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_roles";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            //do i need to add parameters for select roles? 
            // try catch finally
            try
            {
                // open connection
                conn.Open();
                // reader
                var reader = cmd.ExecuteReader();
                // if reader has rows
                if (reader.HasRows)
                {
                    // while reader read
                    while (reader.Read())
                    {
                        // add reader to roles
                        roles.Add(reader.GetString(0));

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // close connection
                conn.Close();
            }
            // return roles
            return roles;
        }
        public List<User> selectUsers()
        {
            // list of users
            List<User> users = new List<User>();
            // connection
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_users";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // try catch finally
            try
            {
                // open connection
                conn.Open();
                // reader
                var reader = cmd.ExecuteReader();
                // if reader has rows
                if (reader.HasRows)
                {
                    // while reader read
                    while (reader.Read())
                    {
                        // user
                        User user = new User();
                        user.UserId = reader.GetInt32(0);
                        user.UserName = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Role = reader.GetString(3);
                        // add user to users list
                        users.Add(user);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // close connection
                conn.Close();
            }
            // return users
            return users;
        }

        public int updateUser(User user)
        {
            // result
            int result = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_update_user";
            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters add with values
            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);
            // try catch finally
            try
            {
                // open connection
                conn.Open();
                // execute command
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // close connection
                conn.Close();
            }
            // return result
            return result;

        }


    }
}

