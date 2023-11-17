using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class AdminManager : AdminManagerInterface
    {
        private AdminAccessorInterface adminAccessor = new AdminAccessor();
        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            users = adminAccessor.selectUsers();
            return users;
            
        }

    }
}
