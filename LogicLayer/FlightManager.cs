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

        public FlightManager(IFlightAccessor flightAccessor)
        {
            this.flightAccessor = flightAccessor;
        }

        public int addNewFlight(Flight flight)
        {
            int result = 0;
            result = flightAccessor.insert(flight);
            return result;
        }

        public int deleteFlight(Flight flight)
        {
            int result = 0;
            result = flightAccessor.deleteFlight(flight);
            return result;
        }

        public int editFlight(Flight flight)
        {
            int result = 0;
            result = flightAccessor.updateFlight(flight);
            return result;
        }

        public List<string> getAllAirPortCodes()
        {
            List<string> result = new List<string>();
            result = flightAccessor.selectAllAirportCode();
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
