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
            // jim's code modified by gishe ask awaab if correct this
            int rows = 0;

            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_authenticate_passenger"; //modified sp_authenticate_employee to sp_authenticate_passenger

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

        public int DeletePassenger(int id)
        {
            //throw new NotImplementedException();
            int rows = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_delete_passenger";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.AddWithValue("@PassengerID", id);
            //process results and delete, update and insert use ExecuteNonQuery
            // ask awaab if correct
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("Passenger not found");
                }
                else if (rows > 1)
                {
                    throw new ApplicationException("Fatal Error: More than one passenger found");
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

        public List<Passenger> GetAllPassengers()
        {
            //throw new NotImplementedException();
            List<Passenger> passengers = new List<Passenger>();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_all_passengers";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameters no parameters
            //process results
            try
            {
                // open the connection
                conn.Open();
                // execute
                var reader = cmd.ExecuteReader();
                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Passenger passenger = new Passenger()
                        {
                            PassengerID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        passengers.Add(passenger);
                    }
                }
                else
                {
                    throw new ArgumentException("Passenger not found");
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
            return passengers;

        }

        public Passenger GetPassengerById(int id)
        {
            //throw new NotImplementedException();
            Passenger passenger = new Passenger();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_passenger_by_id";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters use passengerID
            cmd.Parameters.AddWithValue("@PassengerID", id);
            try
            {
                // open the connection
                conn.Open();
                // execute
                var reader = cmd.ExecuteReader();
                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        passenger.PassengerID = reader.GetInt32(0);
                        passenger.FirstName = reader.GetString(1);
                        passenger.LastName = reader.GetString(2);
                        passenger.PhoneNumber = reader.GetString(3);
                        passenger.Email = reader.GetString(4);
                        passenger.Active = reader.GetBoolean(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Passenger not found");
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
            return passenger;
        }

        public List<Passenger>? SelectAllPassengers()
        {
            //throw new NotImplementedException();
            List<Passenger> passengers = new List<Passenger>();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_all_passengers";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameters no parameters
            //process results
            try
            {
                // open the connection
                conn.Open();
                // execute
                var reader = cmd.ExecuteReader();
                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Passenger passenger = new Passenger()
                        {
                            PassengerID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        passengers.Add(passenger);
                    }
                }
                else
                {
                    throw new ArgumentException("Passenger not found");
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
            return passengers;
        }

        public Passenger SelectPassengerById(int id)
        {
            //throw new NotImplementedException();
            Passenger passenger = new Passenger();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_passenger_by_id";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters use passengerID
            cmd.Parameters.AddWithValue("@PassengerID", id);
            try
            {
                // open the connection
                conn.Open();
                // execute
                var reader = cmd.ExecuteReader();
                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        passenger.PassengerID = reader.GetInt32(0);
                        passenger.FirstName = reader.GetString(1);
                        passenger.LastName = reader.GetString(2);
                        passenger.PhoneNumber = reader.GetString(3);
                        passenger.Email = reader.GetString(4);
                        passenger.Active = reader.GetBoolean(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Passenger not found");
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
            return passenger;
        }
        //from Jim's code
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
            //throw new NotImplementedException();
            List<string> roles = new List<string>();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_roles_by_user"; //ask awaab and passanger has no roles
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters use passengerID
            cmd.Parameters.AddWithValue("@PassengerID", employeeID);
            try
            {
                // open the connection
                conn.Open();
                // execute
                var reader = cmd.ExecuteReader();
                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Passenger not found");
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

        public void UpdatePassenger(Passenger passenger)
        {
            //use execute non query for update, delete and insert
            //throw new NotImplementedException();
            int rows = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_update_passenger";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters use passengerID
            cmd.Parameters.AddWithValue("@PassengerID", passenger.PassengerID);
            cmd.Parameters.AddWithValue("@FirstName", passenger.FirstName);
            cmd.Parameters.AddWithValue("@LastName", passenger.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", passenger.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", passenger.Email);
            cmd.Parameters.AddWithValue("@Active", passenger.Active);
            //process results
            try
            {
                // open the connection
                conn.Open();
                // execute
                rows = cmd.ExecuteNonQuery();
                // process the results
                if (rows == 0)
                {
                    throw new ApplicationException("Passenger not found");
                }
                else if (rows > 1)
                {
                    throw new ApplicationException("Fatal Error: More than one passenger found");
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
            return;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            //throw new NotImplementedException();
            int rows = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_update_passwordhash";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);
            cmd.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
            try
            {
                // open the connection
                conn.Open();
                // execute
                rows = cmd.ExecuteNonQuery();
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

        public void InsertPassenger(Passenger passenger)
        {
            //throw new NotImplementedException();
            int rows = 0; //rows affected
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_insert_passenger";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters use passengerID
            cmd.Parameters.AddWithValue("@PassengerID", passenger.PassengerID);
            cmd.Parameters.AddWithValue("@FirstName", passenger.FirstName);
            cmd.Parameters.AddWithValue("@LastName", passenger.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", passenger.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", passenger.Email);
            cmd.Parameters.AddWithValue("@Active", passenger.Active);
            //process results
            try
            {
                // open the connection
                conn.Open();
                // execute
                rows = cmd.ExecuteNonQuery();
                // process the results
                if (rows == 0)
                {
                    throw new ApplicationException("Passenger not found");
                }
                else if (rows > 1)
                {
                    throw new ApplicationException("Fatal Error: More than one passenger found");
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
            return;

        }

        void IPassengerAccessor.DeletePassenger(int id)
        {
            throw new NotImplementedException();
        }
    }

}
