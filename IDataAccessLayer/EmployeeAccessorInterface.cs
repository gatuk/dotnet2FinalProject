using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface EmployeeAccessorInterface
    {
        int verifyUser(string username, string password);
    }
}
