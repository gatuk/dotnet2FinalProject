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
        public List<Flight> SelectFlightsByAirline(string targetAirline);
        public List<Flight> SelectFlightsByFlightNumber(string targetFlightNumber);
        public List<Flight> SelectFlightsByAirplaneID();
        //tutor added these methods
        public int Insert(Flight flight);
        public List<string> selectAllAirportCode();
        public List<Flight> selectAllFlights();
        public int updateFlight(Flight flight);
        //added this method to delete a flight
        public int deleteFlight(Flight flight);
    }
}
