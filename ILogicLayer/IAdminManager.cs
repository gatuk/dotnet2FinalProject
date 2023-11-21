using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
namespace LogicLayerInterfaces
{
    public interface IAdminManager
    {
        public int addUser(User user);
        public int deleteUser(User? user);
        public List<User> getAllUsers();
        public List<string> getRoles();
        public int updateUser(User user);
    }
}
