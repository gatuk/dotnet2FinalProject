using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private AdminAccessorInterface adminAccessor;
        public AdminManager()
        {
            this.adminAccessor = new AdminAccessor();
        }
        public AdminManager(AdminAccessorInterface adminAccessor)
        {
            this.adminAccessor = adminAccessor;
        }

        public int addUser(User user)
        {
            int result = 0;
            if (user.Password != null)
            {
                user.Password = hashSHA256(user.Password);
            }
            
            result = adminAccessor.insertUser(user);
            return result;
        }

        public int deleteUser(User? user)
        {
            int result = 0;
            result = adminAccessor.deleteUser(user);
            return result;
        }

        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            users = adminAccessor.selectUsers();
            return users;
            
        }

        public List<string> getRoles()
        {
            List<string> roles = new List<string>();
            roles = adminAccessor.selectRoles();
            return roles;
        }

        public int updateUser(User user)
        {
            int result = 0;
            if (user.Password != null)
            {
                user.Password = hashSHA256(user.Password);
            }
            result = adminAccessor.updateUser(user);
            return result;
        }
        private string hashSHA256(string source)
        {
            // defult password is newuser
            string result = "";
            byte[] data;
            using (SHA256 sha256sha = SHA256.Create())
            {
                data = sha256sha.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();
            return result;
        }
    }
}
