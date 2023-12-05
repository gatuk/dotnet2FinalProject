using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
namespace DataAccessFakes
{
    public class FakeFlightAccessor : IFlightAccessor
    {
        private List<Flight> flights;
        private List<string> airportCodes;

        public FakeFlightAccessor(List<Flight> flights)
        {
            this.flights = flights;
            airportCodes = new List<string>();
            generateAirportCodes();
        }

        private void generateAirportCodes()
        {
            for (int i = 1; i < 11; i++)
            {
                airportCodes.Add(i.ToString());
            }
        }

        public int deleteFlight(Flight flight)
        {
            int result = flights.Count;
            flights.Remove(flight);
            return result - flights.Count;
        }

        public int insert(Flight flight)
        {
            int result = flights.Count;
            flights.Add(flight);
            return flights.Count - result;
        }

        public List<string> selectAllAirportCode()
        {
            return airportCodes;
        }

        public List<Flight> selectAllFlights()
        {
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

        public int updateFlight(Flight oldFlight)
        {
            int result = 0;
            foreach (Flight flight in flights)
            {
                if (flight.FlightId == oldFlight.FlightId) {
                    flight.FlightNumber = oldFlight.FlightNumber;
                    flight.DepartureTime = oldFlight.DepartureTime;
                    flight.Departure = oldFlight.Departure;
                    flight.Destination = oldFlight.Destination;
                    flight.ArrivalTime = oldFlight.ArrivalTime;
                    flight.AvailableSeats = oldFlight.AvailableSeats;
                    flight.Price = oldFlight.Price;
                    flight.Airline = oldFlight.Airline;
                    result = 1;
                    break;
                }
            }
            return result;
        }
    }
}
