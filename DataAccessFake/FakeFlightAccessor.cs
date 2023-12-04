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
        public int deleteFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public int insert(Flight flight)
        {
            throw new NotImplementedException();
        }

        public List<string> selectAllAirportCode()
        {
            throw new NotImplementedException();
        }

        public List<Flight> selectAllFlights()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
