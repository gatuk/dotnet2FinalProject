using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface LoginAccessorInterface
    {
        public string verifyUser(string username, string password);
    }
}
