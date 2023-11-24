using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class FlightAccessor : IFlightAccessor
    {

        public int Insert(Flight flight)
        {
            int result = 0;
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_insert_flight";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
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
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_all_airport_codes";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
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
        //added this method to delete or cancel a flight
        public int deleteFlight(Flight flight)
        {
            int result = 0;
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("sp_delete_flight", conn); // Assuming you have sp for deleting a flight.
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
            finally
            {
                conn.Close();
            }

            return result;
        }


        // stated here to be implemented in FlightAccessor.cs
        // the stored procedures named below are not created yet
        public List<Flight> SelectFlightsByDepartureDate()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_departureDate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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
            return flights;
        }
        public List<Flight> SelectFlightsByArrivalDate()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_arrivalDate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        public List<Flight> SelectFlightsByDepartureCity()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_departureCity", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        public List<Flight> SelectFlightsByArrivalCity()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_arrivalCity", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        public List<Flight> SelectFlightsByDepartureTime()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_departureTime", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        public List<Flight> SelectFlightsByArrivalTime()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_arrivalTime", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        // stated here to be implemented in FlightAccessor.cs
        public List<Flight> SelectFlightsByAirline()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flight_by_Airline", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        public List<Flight> SelectFlightsByFlightNumber()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_flightNumber", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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

            return flights;
        }
        public List<Flight> SelectFlightsByAirplaneID()
        {
            List<Flight> flights = new List<Flight>();
            SqlConnection conn = SqlConnectionProvider.GetConnection();
            var cmd = new SqlCommand("select_flights_by_airplaneID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        FlightId = (int)reader["FlightId"],
                        FlightNumber = reader["FlightNumber"].ToString(),
                        Departure = reader["Departure"].ToString(),
                        Destination = reader["Destination"].ToString(),
                        DepartureTime = (DateTime)reader["DepartureTime"],
                        ArrivalTime = (DateTime)reader["ArrivalTime"],
                        AvailableSeats = (int)reader["AvailableSeats"],
                        Price = (decimal)reader["Price"],
                        Airline = reader["Airline"].ToString()
                    };

                    flights.Add(flight);
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
            return flights;
        }

    }
}
