using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;
using DataObjects;
using System.Net;
using DataAccessLayer;

namespace LogicLayerTests
{
    public class TestAdminManager
    {
        private AdminManagerInterface adminManager;
        private AdminAccessorInterface adminAccessor;
        private List<User> users;
        [SetUp]
        public void Setup()
        {
            adminManager = new AdminManager();
            adminAccessor = new AdminAccessor();
            users = new List<User>();
            users.Add(createUser("user1","Admin"));
            users.Add(createUser("user2", "Manager"));
            users.Add(createUser("user3", "Customer"));
            users.Add(createUser("user4", "Admin"));
            users.Add(createUser("user5", "Manager"));
            users.Add(createUser("user6", "Admin"));
            users.Add(createUser("user7", "Customer"));
            users.Add(createUser("user8", "Admin"));
            users.Add(createUser("user9", "Manager"));
            users.Add(createUser("user10", "Customer"));
            adminAccessor = new FakeAdminAccessor(users);
            adminManager = new AdminManager(adminAccessor);
        }

        private User createUser(string data, string role)
        {
           User user = new User();
            user.UserId = 1;
            user.UserName = data;
            user.Password = data;
            user.Role = role;
            return user;
        }

        [Test]
        public void TestAddUser()
        {
            User user = new User();

            user.UserId = 100;
            user.UserName = "Testing";
            user.Password = "Testing";
            user.Role = "Admin";
            int expect = 1;
            int actual = adminManager.addUser(user);
            Assert.That(actual, Is.EqualTo(expect));
        }

        [Test]
        public void TestDeleteUser()
        {
            int expect = 1;
            int actual = adminManager.deleteUser(users[0]);
            Assert.That(actual, Is.EqualTo(expect));
        }
    }
}