using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
	public class PassengerAccessor : IPassengerAccessor
	{
		public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
		{
			int result = 0;
            return result;
		}

        public PassengerVM SelectPassengerVMByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectRolesByUser(int employeeID)
        {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }
    }

}
