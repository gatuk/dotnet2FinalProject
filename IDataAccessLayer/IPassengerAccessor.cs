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

		public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        public int insertPassenger(Passenger passenger);
        public List<Passenger> selectAllPassengers();
        public PassengerVM SelectPassengerVMByEmail(string email);
		public List<string> SelectRolesByUser(int employeeID);
		public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
	}

}

// Path: DataAccessLayer/PassengerAccessor.cs	