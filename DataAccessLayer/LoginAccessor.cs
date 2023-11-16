using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class LoginAccessor : LoginAccessorInterface
    {
        public string verifyUser(string username, string password)
        {
            string role = "testing";
            return role;
        }
    }
}
