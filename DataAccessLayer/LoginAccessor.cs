using DataAccessInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class LoginAccessor : ILoginAccessor
    {//awaab code start
        public string verifyUser(string username, string password)
        {
            string role = "not verify";
            //1- connect to DB
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            //2-store procedure
            SqlCommand cmd = new SqlCommand("sp_verify_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //3- parameters
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        role = reader.GetString(0);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return role;
        }
    }
}
