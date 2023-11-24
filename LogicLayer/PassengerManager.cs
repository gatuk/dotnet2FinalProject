using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class PassengerManager : IPassengerManager
    {
        // dependency inversion for the data provider
        private IPassengerAccessor _passengerAccessor = new PassengerAccessor();


        // the default constructor will use the database
        public PassengerManager()
        {
            _passengerAccessor = new PassengerAccessor();
        }

        public PassengerManager(IPassengerAccessor passengerAccessor)
        {
            _passengerAccessor = passengerAccessor;
        }
        public List<Passenger> GetAllPassengers()
        {
            List<Passenger> passengers = new List<Passenger>();

            try
            {
                passengers = _passengerAccessor.SelectAllPassengers();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return passengers;
        }

        public Passenger GetPassenger(int id)
        {
            Passenger passenger = new Passenger();

            try
            {
                passenger = _passengerAccessor.SelectPassengerById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Passenger not found.", ex);
            }

            return passenger;
        }
        public void UpdatePassenger(Passenger passenger)
        {
            try
            {
                _passengerAccessor.UpdatePassenger(passenger);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Passenger could not be updated.", ex);
            }
        }
        public void DeletePassenger(int id)
        {
            try
            {
                _passengerAccessor.DeletePassenger(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Passenger could not be deleted.", ex);
            }
        }

        public void AddPassenger(Passenger passenger)
        {
            //throw new NotImplementedException();
            try
            {
                _passengerAccessor.InsertPassenger(passenger);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Passenger could not be added.", ex);
            }
        }
    }


}
