using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;
using DataObjects;
using System.Net;
using DataAccessLayer;
using System.Text;
using System.Security.Cryptography;

namespace LogicLayerTests
{
    public class TestLoginManager
    {
        private LoginManagerInterface loginManager;
        private LoginAccessorInterface loginAccessor;
        private List<User> users;
        [SetUp]
        public void Setup()
        {
           users = new List<User>();
            users.Add(createUser(1,"user1","Admin"));
            users.Add(createUser(2, "user2", "Admin"));
            users.Add(createUser(3, "user3", "Admin"));
            users.Add(createUser(4, "user4", "Admin"));
            users.Add(createUser(5, "user5", "Admin"));
            users.Add(createUser(6, "user6", "Admin"));
            users.Add(createUser(7, "user7", "Admin"));
            users.Add(createUser(8, "user8", "Admin"));
            users.Add(createUser(9, "user9", "Admin"));
            users.Add(createUser(10, "user10", "Admin"));
            loginAccessor =  new FakeLoginAccessor(users);
            loginManager = new LoginManager(loginAccessor);
        }

        private User createUser(int id, string username, string role)
        {
           User user = new User();
            user.UserId = id;
            user.UserName = username;
            user.Role = role;
            user.Password = hashSHA256("password");
            return user;
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
        [Test]
        public void TestVerifyUser()
        {
            string expect = "Admin";
            string actual = loginManager.verifyUser("user1", "password");
            Assert.That(actual, Is.EqualTo(expect));
        }
    }
}