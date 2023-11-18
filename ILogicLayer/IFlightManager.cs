using DataObjects;

namespace LogicLayer
{
    public interface IFlightManager
    {
        public int addNewFlight(Flight flight);
        public List<Flight> getAllFlights();
    }
}
