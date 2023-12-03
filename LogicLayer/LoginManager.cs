using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataAccessLayer;
using DataAccessInterfaces;
using DataObjects;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class LoginManager : LoginManagerInterface
    {
        private LoginAccessorInterface loginAccessor = new LoginAccessor();

        public string verifyUser(string username, string password)
        {
            string role = "";
            role = loginAccessor.verifyUser(username,  hashSHA256(password));
            return role;
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
