using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class FlightAccessor : IFlightAccessor
    {
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
    }
}
