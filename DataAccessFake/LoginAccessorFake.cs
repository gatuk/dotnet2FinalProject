using DataAccessInterfaces;
namespace DataAccessFakes
{
    public class LoginAccessorFake : ILoginAccessor
    {
        public string verifyUser(string username, string password)
        {
            //gishe added this comment
            if (username == "admin" && password == "admin")
            {
                return "admin";
            }
            else if (username == "user" && password == "user")
            {
                return "user";
            }
            else
            {
                return null;
            }
        }
    }
}
