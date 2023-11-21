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
        //awaab added these methods
        public List<Flight> selectAllFlights();
        public int insert(Flight flight);
        public List<string> selectAllAirportCode();
        public int updateFlight(Flight flight);
<<<<<<< HEAD
        //added this method to delete a flight
        public int deleteFlight(Flight flight);
=======
>>>>>>> f7d1e3700ebeb0de8e832e2f824d784ae8b58a67
    }
}
