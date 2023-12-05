using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class passengerAccessorFake : IPassengerAccessor
    {
        // create a few fake passenger for testing
        private List<Passenger> passengers ;
        private List<string> passwordHashes = new List<string>();

        public passengerAccessorFake()
        {
            passengers = new List<Passenger>();
            passengers.Add(new Passenger()
            {
                PassengerID = 1,
                FlightID = 1,
                FirstName = "John",
                LastName = "Doe",
                SeatNumber = "1A",
                Email = ""
            });
            passengers.Add(new PassengerVM()
            {
                PassengerID = 2,
                FlightID = 1,
                FirstName = "Jane",
                LastName = "Doe",
                SeatNumber = "1B",
                Email = ""
            });
        }

        public passengerAccessorFake(List<Passenger> passengers)
        {
            this.passengers = passengers;
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            // check for employee records in the fake data
            for (int i = 0; i < passengers.Count; i++)
            {
                if (passwordHashes[i] == passwordHash &&
                    passengers[i].Email == email)
                {
                    numAuthenticated += 1;
                }
            }

            return numAuthenticated;    // should be 1 or 0
        }//return;

        public int insertPassenger(Passenger passenger)
        {
            int result = passengers.Count();
            passengers.Add(passenger);
            return passengers.Count() - result;
        }

        public List<Passenger> selectAllPassengers()
        {
            return passengers;
        }

        public Passenger SelectPassengerVMByEmail(string email)
        {
            Passenger pass = null;

            foreach (var fakePassenger in passengers)
            {
                if (fakePassenger.Email == email)
                {
                    pass = fakePassenger;
                }
            }
            if (pass == null) // no one found
            {
                throw new ApplicationException("Bad Email");
            }
            return pass;
        }

        public List<string> SelectRolesByUser(int PassengerID)
        {
            List<string> roles = new List<string>();

            //foreach (var fakePassenger in passengers)
            //{
            //    if (fakePassenger.PassengerID == PassengerID)
            //    {
            //        roles = fakePassenger.Roles;
            //        break;
            //    }
            //}
            return roles;

        }

        public int updatePassenger(Passenger passenger)
        {
            int result = 0;
            foreach (Passenger item in passengers)
            {
                if (passenger.PassengerID == item.PassengerID)
                {
                    item.FlightID = passenger.FlightID;
                    item.FirstName = passenger.FirstName; 
                    item.LastName = passenger.LastName;
                    item.SeatNumber = passenger.SeatNumber;
                    item.Email = passenger.Email;
                    item.PhoneNumber = passenger.PhoneNumber;
                    item.Address = passenger.Address;
                    item.City = passenger.City;
                    item.State = passenger.State;
                    item.ZipCode = passenger.ZipCode;
                    item.IsCheckedIn = passenger.IsCheckedIn;
                    item.IsMinor = passenger.IsMinor;
                    item.IsSpecialNeeds = passenger.IsSpecialNeeds;
                    item.Active = passenger.Active;
                    result = 1;
                    break;
                }
            }
            return result;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < passengers.Count; i++)
            {
                if (passengers[i].Email == email)
                {
                    if (passwordHashes[i] == oldPasswordHash)
                    {
                        passwordHashes[i] = newPasswordHash;
                        rows += 1;
                    }
                }
            }
            if (rows != 1) // no one found
            {
                throw new ApplicationException("Bad email or password");
            }

            return rows;

        }
    }
}
