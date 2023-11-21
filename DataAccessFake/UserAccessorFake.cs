using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        public int deleteUser(User? user)
        {
            throw new System.NotImplementedException();
        }

        public int insertUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<string> selectRoles()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<User> selectUsers()
        {
            throw new System.NotImplementedException();
        }

        public int updateUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
