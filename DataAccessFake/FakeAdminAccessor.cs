using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class FakeAdminAccessor : AdminAccessorInterface
    {
        private List<User> users;
        public FakeAdminAccessor()
        {
            this.users = new List<User>();
        }
        public FakeAdminAccessor(List<User> users)
        {
            this.users = users;
        }

        public int deleteUser(User? user)
        {
            throw new NotImplementedException();
        }

        public int insertUser(User user)
        {
            int result = users.Count;
            users.Add(user);
            return users.Count - result;
        }

        public List<string> selectRoles()
        {
            throw new NotImplementedException();
        }

        public List<User> selectUsers()
        {
            throw new NotImplementedException();
        }

        public int updateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
