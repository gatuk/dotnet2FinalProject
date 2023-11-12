using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class passengerAccessorFake : IPassengerAccessor
    {
        // create a few fake passenger for testing
        private List<PassengerVM> fakePassengers = new List<PassengerVM>();
        private List<string> passwordHashes = new List<string>();

        public passengerAccessorFake()
        {
            fakePassengers.Add(new PassengerVM()
            {
                PassengerID = 1,
                FlightID = 1,
                FirstName = "John",
                LastName = "Doe",
                SeatNumber = "1A",
                Email = "",
                Roles = new List<string>()
            });
            fakePassengers.Add(new PassengerVM()
            {
                PassengerID = 2,
                FlightID = 1,
                FirstName = "Jane",
                LastName = "Doe",
                SeatNumber = "1B",
                Email = "",
                Roles = new List<string>()
            });
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            // check for employee records in the fake data
            for (int i = 0; i < fakePassengers.Count; i++)
            {
                if (passwordHashes[i] == passwordHash &&
                    fakePassengers[i].Email == email)
                {
                    numAuthenticated += 1;
                }
            }

            return numAuthenticated;    // should be 1 or 0
        }

        public PassengerVM SelectPassengerVMByEmail(string email)
        {
            PassengerVM pass = null;

            foreach (var fakePassenger in fakePassengers)
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

            foreach (var fakePassenger in fakePassengers)
            {
                if (fakePassenger.PassengerID == PassengerID)
                {
                    roles = fakePassenger.Roles;
                    break;
                }
            }
            return roles;

        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakePassengers.Count; i++)
            {
                if (fakePassengers[i].Email == email)
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
