using DataAccessInterfaces;
using DataObjects;


namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        private List<User> users = new List<User>();
        public int deleteUser(User? user)
        {
            //throw new NotImplementedException();    // This is a stub
            // test conditions if user is null or not
            int result = 0;
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }
            else
            {
                int index = users.FindIndex(u => u.UserId == user.UserId);
                if (index == -1)
                {
                    throw new ApplicationException("User not found.");
                }
                else
                {
                    users.RemoveAt(index);
                    result = 1;
                }
            }
            return result;

        }

        public int insertUser(User user)
        {
            //throw new NotImplementedException();    // This is a stub
            // test conditions if user is already in the list
            int result = 0;
            int index = users.FindIndex(u => u.UserId == user.UserId);
            if (index != -1)
            {
                throw new ApplicationException("User already exists.");
            }
            else
            {
                users.Add(user);
                result = 1;
            }
            return result;

        }

        public List<string> selectRoles()
        {
            //throw new NotImplementedException();    // This is a stub
            // select all roles from the list
            List<string> roles = new List<string>();
            foreach (User user in users)
            {
                roles.Add(user.UserRole);
            }
            return roles;
        }
        public List<User> selectUsers()
        {
            //throw new NotImplementedException();// This is a stub
            // select all users from the list
            List<User> users = new List<User>();
            foreach (User user in users)
            {
                users.Add(user);
            }
            return users;
        }
        public int updateUser(User user)
        {
            //throw new NotImplementedException();    // This is a stub
            // check if user exists in the list and update
            int result = 0;
            int index = users.FindIndex(u => u.UserId == user.UserId);
            if (index == -1)
            {
                throw new ApplicationException("User not found.");
            }
            else
            {
                users[index] = user;
                result = 1;
            }
            return result;
        }
    }
}
