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

        public List<Passenger> getAllPassengers()
        {
            List<Passenger> passengers = new List<Passenger>();
			passengers = _passengerAccessor.selectAllPassengers();
			return passengers;
        }
    }
}
