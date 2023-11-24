using DataObjects;
namespace DataAccessInterfaces
{
    public interface IPassengerAccessor
    {
        //Passenger RetrievePassengerByID(int passengerID);
        //List<Passenger> RetrievePassengersByFlightID(int flightID);
        //int InsertPassenger(Passenger passenger);
        //int UpdatePassenger(Passenger oldPassenger, Passenger newPassenger);
        //int DeletePassenger(Passenger passenger);
        //from Jim's employee code
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
        PassengerVM SelectPassengerVMByEmail(string email);
        //gishe added this
        List<Passenger> GetAllPassengers();
        Passenger GetPassengerById(int id);
        void InsertPassenger(Passenger passenger);
        List<Passenger>? SelectAllPassengers();
        Passenger SelectPassengerById(int id);

        List<string> SelectRolesByUser(int employeeID);
        void DeletePassenger(int id);
        void UpdatePassenger(Passenger passenger);

    }

}

// Path: DataAccessLayer/PassengerAccessor.cs	