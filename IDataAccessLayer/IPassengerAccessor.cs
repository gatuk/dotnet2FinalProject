﻿using DataObjects;
namespace DataAccessInterfaces
{
	public interface IPassengerAccessor
	{
		//Passenger RetrievePassengerByID(int passengerID);
		//List<Passenger> RetrievePassengersByFlightID(int flightID);
		//int InsertPassenger(Passenger passenger);
		//int UpdatePassenger(Passenger oldPassenger, Passenger newPassenger);
		//int DeletePassenger(Passenger passenger);

		int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
		PassengerVM SelectPassengerVMByEmail(string email);
		List<string> SelectRolesByUser(int employeeID);
		int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
	}

}

// Path: DataAccessLayer/PassengerAccessor.cs	