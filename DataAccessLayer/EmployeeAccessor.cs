using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;
namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int rows = 0;

            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_authenticate_employee";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to add parameters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // we need to set the parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that everything is set up, we can open the connection
            // and execute the command in a try-catch-finally 
            try
            {
                // open the connection
                conn.Open();

                // execute the command and capture the result
                rows = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public EmployeeVM SelectEmployeeVMByEmail(string email)
        {
            EmployeeVM employeeVM = new EmployeeVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // parameter values
            cmd.Parameters["@Email"].Value = email;

            try
            {
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.FirstName = reader.GetString(1);
                        employeeVM.LastName = reader.GetString(2);
                        employeeVM.Phone = reader.GetString(3);
                        employeeVM.Email = reader.GetString(4);
                        employeeVM.Active = reader.GetBoolean(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Employee not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employeeVM;
        }

        public List<string> SelectRolesByEmployee(int employeeID)
        {
            List<string> roles = new List<string>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_roles_by_employeeID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_update_passwordHash";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to add parameters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);

            // we need to set the parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;

            // now that everything is set up, we can open the connection
            // and execute the command in a try-catch-finally 
            try
            {
                // open the connection
                conn.Open();

                // an update is executed nonquery - returns and int
                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    // treat a failed update as an exception
                    throw new ArgumentException("Bad email or password");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
        //awaab added this method
        public int verifyUser(string username, string password)
        {
            int id = 0;
            // 1-connect to database
            SqlConnection conn = SqlConnectionProvider.GetConnection();

            // 2- verify the store procedure
            var cmd = new SqlCommand("sp_verify_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            // 3- verify the parameters of the store procedure
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            // 4- connect and execute
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }

            return id;
        }

    }
}
