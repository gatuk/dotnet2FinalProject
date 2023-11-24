using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class FlightManager : IFlightManager
    {

        private IFlightAccessor flightAccessor;
        public FlightManager()
        {
            flightAccessor = new FlightAccessor();
        }

        public FlightManager(FlightAccessorFake flightAccessorFake)
        {
            flightAccessor = flightAccessorFake;
        }

        public int addNewFlight(Flight flight)
        {
            int result = 0;
            result = flightAccessor.Insert(flight);
            return result;
        }

        public int editFlight(Flight flight)
        {
            int result = 0;
            result = flightAccessor.updateFlight(flight);
            return result;
        }
        public int deleteFlight(Flight flight)
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

        public int deleteFlight(int flightId)
        {
            throw new NotImplementedException();
        }
    }
}






//what logic is supposed to do for the flight manager
// add new flight, get all flights, get flight by id, update flight, delete flight
// what is the purpose of logic layer and data access layer
