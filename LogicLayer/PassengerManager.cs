using DataAccessLayer;
using DataAccessInterfaces;
using DataObjects;

namespace LogicLayer
{
	public class PassengerManager : IPassengerManager
	{
        // dependency inversion for the data provider
        private IPassengerAccessor _passengerAccessor = null;		


		// the default constructor will use the database
		public PassengerManager()
		{
			_passengerAccessor = new PassengerAccessor();
		}

		public PassengerManager(IPassengerAccessor passengerAccessor)
		{
			_passengerAccessor = passengerAccessor;
		}

        public int addPassenger(Passenger passenger)
        {
			int result = 0;
			result = _passengerAccessor.insertPassenger(passenger);
			return result;
        }

        public List<Passenger> getAllPassengers()
        {
            List<Passenger> passengers = new List<Passenger>();
			passengers = _passengerAccessor.selectAllPassengers();
			return passengers;
        }

        public int updatePassenger(Passenger passenger)
        {
			int result = 0;
			result = _passengerAccessor.updatePassenger(passenger);
			return result;
        }
    }
}
