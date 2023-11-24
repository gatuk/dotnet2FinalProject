using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        //tutor added this
        public int deleteUser(User? user);
        public int insertUser(User user);
        public List<string> selectRoles();
        public List<User> selectUsers();
        public int updateUser(User user);


    }
}
