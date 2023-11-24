using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class AdminAccessorFake : IAdminAccessor
    {
        // create a few fake employees for testing
        //gishe added jim code to this file
        private List<AdminVM> fakeAdmins = new List<AdminVM>();
        private List<string> passwordHashes = new List<string>();
        public AdminAccessorFake()
        {
            fakeAdmins.Add(new AdminVM()
            {
                AdminID = 1,
                GivenName = "Tess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "Tess@gmail.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeAdmins.Add(new AdminVM()
            {
                AdminID = 2,
                GivenName = "Bess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "bess@gmail.com",
                Active = true,
                Roles = new List<string>()
            });
            //passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            //passwordHashes.Add("badhash");
            //passwordHashes.Add("badhash");

            //fakeAdmins[0].Roles.Add("TestRole1");
            //fakeAdmins[0].Roles.Add("TestRole2");

        }
        public AdminVM SelectAdminVMByEmail(string email)
        {
            AdminVM admin = null;

            foreach (var fakeAdmin in fakeAdmins)
            {
                if (fakeAdmin.Email == email)
                {
                    admin = fakeAdmin;
                }
            }
            if (admin == null) // no one found
            {
                throw new ApplicationException("Bad Email");
            }
            return admin;
        }
        public List<string> SelectRolesByAdmin(int adminID)
        {
            List<string> roles = new List<string>();

            foreach (var fakeAdmin in fakeAdmins)
            {
                if (fakeAdmin.AdminID == adminID)
                {
                    roles = fakeAdmin.Roles;
                    break;
                }
            }
            return roles;
        }
        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakeAdmins.Count; i++)
            {
                if (fakeAdmins[i].Email == email)
                {
                    if (passwordHashes[i] == oldPasswordHash)
                    {
                        passwordHashes[i] = newPasswordHash;
                        rows += 1;
                    }
                }
            }
            if (rows != 1) // no one found
            {
                throw new ApplicationException("Bad email or password");
            }

            return rows;
        }
        public int deleteUser(User? user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            // Find the user to delete based on user ID
            var userToDelete = fakeAdmins.FirstOrDefault(admin => admin.AdminID == user.UserId);

            if (userToDelete != null)
            {
                fakeAdmins.Remove(userToDelete);
                return 1; // User deleted successfully
            }

            return 0; // User not found or deletion failed
        }
        public int insertUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            // Generate a unique ID for the new user (You can use a different method for generating IDs)
            int newUserId = fakeAdmins.Max(admin => admin.AdminID) + 1;

            // Create a new AdminVM object from the User and assign a unique ID
            var newAdmin = new AdminVM
            {
                AdminID = newUserId,
                UserName = user.UserName,
                GivenName = user.GivenName,
                FamilyName = user.FamilyName,
                Phone = user.Phone,
                Email = user.Email,
                Active = true,
                Roles = new List<string>()
            };

            // Add the new user to the fakeAdmins list
            fakeAdmins.Add(newAdmin);

            return 1; // User inserted successfully
        }

        public List<string> selectRoles()
        {
            // Retrieve a list of all unique roles from the fakeAdmins list
            var allRoles = fakeAdmins.SelectMany(admin => admin.Roles).Distinct().ToList();
            return allRoles;
        }

        public List<User> selectUsers()
        {
            // Retrieve a list of all users from the fakeAdmins list
            var allUsers = fakeAdmins.Select(admin => new User
            {
                UserId = admin.AdminID,
                UserName = admin.UserName,
                GivenName = admin.GivenName,
                FamilyName = admin.FamilyName,
                Phone = admin.Phone,
                Email = admin.Email,
                //Active = admin.Active
            }).ToList();
            return allUsers;
        }

        public int updateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            // Find the user to update based on user ID
            var userToUpdate = fakeAdmins.FirstOrDefault(admin => admin.AdminID == user.UserId);

            if (userToUpdate != null)
            {
                // Update the user's properties
                userToUpdate.UserName = user.UserName;
                userToUpdate.GivenName = user.GivenName;
                userToUpdate.FamilyName = user.FamilyName;
                userToUpdate.Phone = user.Phone;
                userToUpdate.Email = user.Email;
                userToUpdate.Active = user.Active;

                return 1; // User updated successfully
            }

            return 0; // User not found or update failed
        }

    }
}



