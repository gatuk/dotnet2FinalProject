using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;
using DataObjects;
using System.Net;
using DataAccessLayer;

namespace LogicLayerTests
{
    public class TestFlightManager
    {
        private IFlightManager flightManager;
        private IFlightAccessor flightAccessor;
        private List<Flight> flights;
        [SetUp]
        public void Setup()
        {
            flights = new List<Flight>();
            flights.Add(createFlight("user1"));
            flights.Add(createFlight("user2"));
            flights.Add(createFlight("user3"));
            flights.Add(createFlight("user4"));
            flights.Add(createFlight("user5"));
            flights.Add(createFlight("user6"));
            //flightAccessor = new FakeAdminAccessor(flights);
            //adminManager = new AdminManager(flightAccessor);
        }

        private User createFlight(string data)
        {
            Flight flight = new Flight();
            return flight;
        }

        //[Test]
        //public void TestAddUser()
        //{
        //    User user = new User();

        //    user.UserId = 100;
        //    user.UserName = "Testing";
        //    user.Password = "Testing";
        //    user.Role = "Admin";
        //    int expect = 1;
        //    int actual = adminManager.addUser(user);
        //    Assert.That(actual, Is.EqualTo(expect));
        //}

        //[Test]
        //public void TestDeleteUser()
        //{
        //    int expect = 1;
        //    int actual = adminManager.deleteUser(flights[0]);
        //    Assert.That(actual, Is.EqualTo(expect));
        //}
        //[Test]
        //public void TestGetAllUsers()
        //{
        //    int expect = 10;
        //    int actual = adminManager.getAllUsers().Count;
        //    Assert.That(actual, Is.EqualTo(expect));
        //}
        //[Test]
        //public void TestGetRoles()
        //{
        //    int expect = roles.Count;
        //    int actual = adminManager.getRoles().Count;
        //    Assert.That(actual, Is.EqualTo(expect));
        //}
        //[Test]
        //public void TestUpdateUser()
        //{
        //    User user = flights[0];
        //    user.Password = "Test";
        //    int expect = 1;
        //    int actual = adminManager.updateUser(user);
        //    Assert.That(actual, Is.EqualTo(expect));
        //}
    }
}