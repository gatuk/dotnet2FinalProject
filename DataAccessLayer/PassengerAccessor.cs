using DataAccessInterfaces;
namespace DataAccessLayer
{
	public class PassengerAccessor : IPassengerAccessor
	{
		public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
		{
			int result = 0;
		}
	}

}
