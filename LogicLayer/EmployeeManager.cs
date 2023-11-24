using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;
using System.Security.Cryptography;     // provides hash libraries
using System.Text;
namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        // dependency inversion for the data provider
        private IEmployeeAccessor _employeeAccessor = null;
        public int verifyUser(string username, string password)
        {
            int id = 0;
            id = _employeeAccessor.verifyUser(username, password);
            return id;
        }

        //gishe added code below here and modified jim code 
        // the default constructor will use the database
        public EmployeeManager()    // uses the database
        {
            _employeeAccessor = new EmployeeAccessor();
        }
        // the optional constructor can accept any data provider (fake)
        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }

        public bool AuthenticateEmployee(string email, string password)
        {
            bool result = false;

            password = HashSha256(password);

            result = (1 == _employeeAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password));

            return result;
        }

        public EmployeeVM GetEmployeeVMByEmail(string email)
        {
            EmployeeVM employeeVM = null;

            try
            {
                employeeVM = _employeeAccessor.SelectEmployeeVMByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Employee not found", ex);
            }

            return employeeVM;
        }

        public List<string> GetRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            try
            {
                roles = _employeeAccessor.SelectRolesByEmployee(employeeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roles;
        }

        public string HashSha256(string source)
        {
            string hashValue = "";

            // hash functions run at the bits and bytes level
            // so we create a byte array
            byte[] data;

            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                // hash the source string
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // create an output stringbuilder object
            var s = new StringBuilder();

            // loop through the byte array and build a return string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2")); // outputs the byte as two hex digits
            }

            hashValue = s.ToString();
            return hashValue;
        }

        public EmployeeVM LoginEmployee(string email, string password)
        {
            EmployeeVM employeeVM = null;

            try
            {
                if (AuthenticateEmployee(email, password))
                {
                    employeeVM = GetEmployeeVMByEmail(email);
                    employeeVM.Roles = GetRolesByEmployeeID(employeeVM.EmployeeID);
                }
                else // throw an exception
                {
                    throw new ArgumentException("Bad email or password");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Authentication Failed", ex);
            }

            return employeeVM;
        }

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = HashSha256(oldPassword);
            newPassword = HashSha256(newPassword);

            try
            {
                result = (1 == _employeeAccessor.UpdatePasswordHash(email, oldPassword, newPassword));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("User or password not found.", ex);
            }

            return result;
        }
    }
}
