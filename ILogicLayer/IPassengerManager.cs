using DataObjects;
namespace LogicLayer
{
    public interface IPassengerManager
    {

        List<Passenger> GetAllPassengers();
        Passenger GetPassenger(int id);
        void AddPassenger(Passenger passenger);
        void UpdatePassenger(Passenger passenger);
        void DeletePassenger(int id);
    }
}
