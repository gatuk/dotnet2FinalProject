using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class AdminAccessorFake : IAdminAccessor
    {
        // create a few fake employees for testing
        //gishe added jim code to this file
        private List<Admin> _admins = new List<Admin>();
        public AdminAccessorFake()
        {
            _admins.Add(new Admin()
            {
                Id = 1000000,
                Name = "Jim"
            });

        }

        public int deleteUser(User? user)
        {
            throw new NotImplementedException();

        }

        public int insertUser(User user)
        {
            throw new NotImplementedException();

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
