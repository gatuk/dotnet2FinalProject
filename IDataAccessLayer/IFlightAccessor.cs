using DataObjects;

namespace DataAccessInterfaces
{
	public interface IFlightAccessor
	{
		public List<Flight> SelectFlightsByDepartureDate();
		public List<Flight> SelectFlightsByArrivalDate();
		public List<Flight> SelectFlightsByDepartureCity();
		public List<Flight> SelectFlightsByArrivalCity();
		public List<Flight> SelectFlightsByDepartureTime();
		public List<Flight> SelectFlightsByArrivalTime();
		public List<Flight> SelectFlightsByAirline();
		public List<Flight> SelectFlightsByFlightNumber();
		public List<Flight> SelectFlightsByAirplaneID();
        public List<Flight> selectAllFlights();
        public int insert(Flight flight);
        public List<string> selectAllAirportCode();
    }
}
