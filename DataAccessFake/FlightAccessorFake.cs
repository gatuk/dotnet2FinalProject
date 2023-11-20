using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
	public class FlightAccessorFake : IFlightAccessor
	{
		public List<Flight> SelectFlightsByDepartureDate()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByArrivalDate()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByDepartureCity()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByArrivalCity()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByDepartureTime()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByArrivalTime()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByAirline()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByFlightNumber()
		{
			throw new NotImplementedException();
		}

		public List<Flight> SelectFlightsByAirplaneID()
		{
			throw new NotImplementedException();
		}

        public List<Flight> selectAllFlights()
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
    }
}
