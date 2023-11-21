using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class PassengerAccessor : IPassengerAccessor
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

        //public PassengerVM SelectPassengerVMByEmail(string email)
        //{
        //    throw new NotImplementedException();
        //}
        public PassengerVM SelectPassengerVMByEmail(string email)
        {
            PassengerVM PassengerVM = new PassengerVM();

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
                        PassengerVM.PassengerID = reader.GetInt32(0);
                        PassengerVM.FirstName = reader.GetString(1);
                        PassengerVM.LastName = reader.GetString(2);
                        PassengerVM.PhoneNumber = reader.GetString(3);
                        PassengerVM.Email = reader.GetString(4);
                        PassengerVM.Active = reader.GetBoolean(5);
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
            return PassengerVM;
        }
        public List<string> SelectRolesByUser(int employeeID)
        {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }
    }

}
