using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IAdminAccessor
    {
        public int deleteUser(User? user);
        public int insertUser(User user);
        public List<string> selectRoles();
        public List<User> selectUsers();
        public int updateUser(User user);
    }
}
