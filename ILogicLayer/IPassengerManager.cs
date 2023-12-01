using DataObjects;

namespace LogicLayer
{
    public interface IPassengerManager
    {
        public int addPassenger(Passenger passenger);
        public List<Passenger> getAllPassengers();
        public int updatePassenger(Passenger passenger);
    }
}
