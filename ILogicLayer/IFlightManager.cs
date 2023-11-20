using DataObjects;

namespace LogicLayer
{
    public interface IFlightManager
    {
        public int addNewFlight(Flight flight);
        public List<string> getAllAirPortCodes();
        public List<Flight> getAllFlights();
    }
}
