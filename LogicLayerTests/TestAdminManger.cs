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
        private List<string> roles;
        [SetUp]
        public void Setup()
        {
            adminManager = new AdminManager();
            adminAccessor = new AdminAccessor();
            roles = new List<string>();
            roles.Add("admin");
            roles.Add("Manager");
            roles.Add("Customer");
            users = new List<User>();
            users.Add(createUser("user1", roles[0]));
            users.Add(createUser("user2", roles[1]));
            users.Add(createUser("user3", roles[2]));
            users.Add(createUser("user4", roles[0]));
            users.Add(createUser("user5", roles[1]));
            users.Add(createUser("user6", roles[2]));
            users.Add(createUser("user7", roles[0]));
            users.Add(createUser("user8", roles[1]));
            users.Add(createUser("user9", roles[2]));
            users.Add(createUser("user10", roles[0]));
            adminAccessor = new FakeAdminAccessor(users,roles);
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
        [Test]
        public void TestGetAllUsers()
        {
            int expect = 10;
            int actual = adminManager.getAllUsers().Count;
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestGetRoles()
        {
            int expect = roles.Count;
            int actual = adminManager.getRoles().Count;
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestUpdateUser()
        {
            User user = users[0];
            user.Password = "Test";
            int expect = 1;
            int actual = adminManager.updateUser(user);
            Assert.That(actual, Is.EqualTo(expect));
        }
    }
}