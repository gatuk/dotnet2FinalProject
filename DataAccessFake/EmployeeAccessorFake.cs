using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class EmployeeAccessorFake : IEmployeeAccessor
    {
        // create a few fake employees for testing
        //gishe added jim code to this file
        private List<EmployeeVM> fakeEmployees = new List<EmployeeVM>();
        private List<string> passwordHashes = new List<string>();

        public EmployeeAccessorFake()
        {
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 1,
                FirstName = "Tess",
                LastName = "Data",
                Phone = "1234567890",
                Email = "tess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 2,
                FirstName = "Bess",
                LastName = "Data",
                Phone = "1234567890",
                Email = "bess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 3,
                FirstName = "Jess",
                LastName = "Data",
                Phone = "1234567890",
                Email = "jess@company.com",
                Active = true,
                Roles = new List<string>()
            });

            passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordHashes.Add("badhash");
            passwordHashes.Add("badhash");

            fakeEmployees[0].Roles.Add("TestRole1");
            fakeEmployees[0].Roles.Add("TestRole2");
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            // check for employee records in the fake data
            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (passwordHashes[i] == passwordHash &&
                    fakeEmployees[i].Email == email)
                {
                    numAuthenticated += 1;
                }
            }

            return numAuthenticated;    // should be 1 or 0
        }

        public EmployeeVM SelectEmployeeVMByEmail(string email)
        {
            EmployeeVM emp = null;

            foreach (var fakeEmployee in fakeEmployees)
            {
                if (fakeEmployee.Email == email)
                {
                    emp = fakeEmployee;
                }
            }
            if (emp == null) // no one found
            {
                throw new ApplicationException("Bad Email");
            }
            return emp;
        }

        public List<string> SelectRolesByEmployee(int employeeID)
        {
            List<string> roles = new List<string>();

            foreach (var fakeEmployee in fakeEmployees)
            {
                if (fakeEmployee.EmployeeID == employeeID)
                {
                    roles = fakeEmployee.Roles;
                    break;
                }
            }
            return roles;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (fakeEmployees[i].Email == email)
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
        public int verifyUser(string username, string password)
        {
            //throw new NotImplementedException();
            // This is not correct , but it is just for testing and ask awaab
            int result = 0;
            if (username == "customer" && password == "customer")
            {
                result = 1;
            }
            else if (username == "user" && password == "user")
            {
                result = 2;
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}
