using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class EmployeeManger : EmployeeMangerInterface
    {
        private EmployeeAccessorInterface employeeAccessor = new EmployeeAccessor();
        public int verifyUser(string username, string password)
        {
            int id = 0;
            id = employeeAccessor.verifyUser(username, password);
            return id;
        }
    }
}
