using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
namespace LogicLayerInterfaces
{
    public interface AdminManagerInterface
    {
        public int addUser(User user);
        public List<User> getAllUsers();
        public List<string> getRoles();
    }
}
