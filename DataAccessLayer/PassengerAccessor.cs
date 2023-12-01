using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
	public class PassengerAccessor : IPassengerAccessor
	{
		public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
		{
			int result = 0;
            return result;
		}

        public int insertPassenger(Passenger passenger)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_insert_passenger", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FlightID", passenger.FlightID);
            cmd.Parameters.AddWithValue("@FirstName", passenger.FirstName);
            cmd.Parameters.AddWithValue("@LastName", passenger.LastName);
            cmd.Parameters.AddWithValue("@SeatNumber", passenger.SeatNumber);
            cmd.Parameters.AddWithValue("@Email", passenger.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", passenger.PhoneNumber);
            cmd.Parameters.AddWithValue("@Address", passenger.Address);
            cmd.Parameters.AddWithValue("@City", passenger.City);
            cmd.Parameters.AddWithValue("@State", passenger.State);
            cmd.Parameters.AddWithValue("@ZipCode", passenger.ZipCode);
            cmd.Parameters.AddWithValue("@IsCheckedIn", passenger.IsCheckedIn);
            cmd.Parameters.AddWithValue("@IsMinor", passenger.IsMinor);
            cmd.Parameters.AddWithValue("@IsSpecialNeeds", passenger.IsSpecialNeeds);
            cmd.Parameters.AddWithValue("@Active", passenger.Active);


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

        public List<Passenger> selectAllPassengers()
        {
            List<Passenger> passengers = new List<Passenger>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_all_passengers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Passenger passenger = new Passenger();
                        passenger.PassengerID = reader.GetInt32(0);
                        passenger.FlightID = reader.GetInt32(1);
                        passenger.FirstName = reader.GetString(2);
                        passenger.LastName = reader.GetString(3);
                        passenger.SeatNumber = reader.GetString(4);
                        passenger.Email = reader.GetString(5);
                        passenger.PhoneNumber = reader.GetString(6);
                        passenger.Address = reader.GetString(7);
                        passenger.City = reader.GetString(8);
                        passenger.State = reader.GetString(9);
                        passenger.ZipCode = reader.GetInt32(10);
                        passenger.IsCheckedIn = reader.GetBoolean(11);
                        passenger.IsMinor = reader.GetBoolean(12);
                        passenger.IsSpecialNeeds = reader.GetBoolean(13);
                        passenger.Active = reader.GetBoolean(14);
                        passengers.Add(passenger);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return passengers;
        }

        public PassengerVM SelectPassengerVMByEmail(string email)
        {
            throw new NotImplementedException();
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
