using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class FlightAccessor : IFlightAccessor
    {
        public int deleteFlight(Flight flight)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_delete_flight", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FlightId", flight.FlightId);
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

        public int insert(Flight flight)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_insert_flight", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
            cmd.Parameters.AddWithValue("@Departure", flight.Departure);
            cmd.Parameters.AddWithValue("@Destination", flight.Destination);
            cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
            cmd.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
            cmd.Parameters.AddWithValue("@AvailableSeats", flight.AvailableSeats);
            cmd.Parameters.AddWithValue("@Price", flight.Price);
            cmd.Parameters.AddWithValue("@Airline", flight.Airline);
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

        public List<string> selectAllAirportCode()
        {
            List<string> airPortCodes = new List<string>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_all_airport_codes", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        airPortCodes.Add(reader.GetString(0));
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return airPortCodes;
        }

        public List<Flight> selectAllFlights()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_all_flights", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Flight flight = new Flight();
                        flight.FlightId = reader.GetInt32(0);
                        flight.FlightNumber = reader.GetString(1);
                        flight.Departure = reader.GetString(2);
                        flight.Destination = reader.GetString(3);
                        flight.DepartureTime = reader.GetDateTime(4);
                        flight.ArrivalTime = reader.GetDateTime(5);
                        //flight.Capacity = reader.GetInt32(6);
                        flight.AvailableSeats = reader.GetInt32(6);
                        flight.Price = reader.GetDecimal(7);
                        flight.Airline = reader.GetString(8);
                        //flight.Aircraft = reader.GetString(9);
                        //flight.Status = reader.GetString(9);
                        //flight.Active = reader.GetBoolean(9);
                        flights.Add(flight);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return flights;
        }

        public List<Flight> SelectFlightsByAirline()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByAirplaneID()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByArrivalCity()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByArrivalDate()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByArrivalTime()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByDepartureCity()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByDepartureDate()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByDepartureTime()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByFlightNumber()
        {
            throw new NotImplementedException();
        }

        public int updateFlight(Flight flight)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_update_flight", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FlightId", flight.FlightId);
            cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
            cmd.Parameters.AddWithValue("@Departure", flight.Departure);
            cmd.Parameters.AddWithValue("@Destination", flight.Destination);
            cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
            cmd.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
            cmd.Parameters.AddWithValue("@AvailableSeats", flight.AvailableSeats);
            cmd.Parameters.AddWithValue("@Price", flight.Price);
            cmd.Parameters.AddWithValue("@Airline", flight.Airline);
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
