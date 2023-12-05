using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
namespace DataAccessFakes
{
    public class FakeLoginAccessor : LoginAccessorInterface
    {
        private List<User> _users;
        public FakeLoginAccessor() { 
            _users = new List<User>();
        }

        public FakeLoginAccessor(List<User> users)
        {
            _users = users;
        }

        public string verifyUser(string username, string password)
        {
            string result = "";
            foreach (var user in _users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    if (user.Role != null)
                    {
                        result = user.Role;
                    }                    
                    break;
                }
            }
            return result.ToString();
        }
    }
}
