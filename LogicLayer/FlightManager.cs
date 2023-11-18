using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
namespace LogicLayer
{
    public class FlightManager : IFlightManager
    {

        private IFlightAccessor flightAccessor;
        public FlightManager() { 
            flightAccessor = new FlightAccessor();
        }

        public int addNewFlight(Flight flight)
        {
            int result = 0;
            result = flightAccessor.insert(flight);
            return result;
        }

        public List<Flight> getAllFlights()
        {
            List<Flight> flights = new List<Flight>();
            flights = flightAccessor.selectAllFlights();
            return flights;
        }
    }
}
