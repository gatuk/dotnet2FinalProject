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
            else if (username == "airlinestaff" && password == "airlinestaff")
            {
                return "airlinestaff";
            }
            else if (username == "customer" && password == "customer")
            {
                return "customer";
            }
            else
            {
                return null;
            }
        }
    }
}
