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
    public class TestPassengerManager
    {
       private IPassengerAccessor _passengerAccessor;
        private IPassengerManager _passengerManager;
        private List<Passenger> passengers;
        [SetUp]
        public void Setup()
        {
           passengers = new List<Passenger>();
            passengers.Add(createPassenger(1,"passenger1"));
            passengers.Add(createPassenger(2, "passenger2"));
            passengers.Add(createPassenger(3, "passenger3"));
            passengers.Add(createPassenger(4, "passenger4"));
            passengers.Add(createPassenger(5, "passenger5"));
            passengers.Add(createPassenger(6, "passenger6"));
            passengers.Add(createPassenger(7, "passenger7"));
            passengers.Add(createPassenger(8, "passenger8"));
            passengers.Add(createPassenger(9, "passenger9"));
            passengers.Add(createPassenger(10, "passenger10"));
            _passengerAccessor = new passengerAccessorFake(passengers);
            _passengerManager = new PassengerManager(_passengerAccessor);
        }

        private Passenger createPassenger(int id, string data)
        {
            Passenger passenger = new Passenger();
            passenger.PassengerID = id;
            passenger.FlightID = id;
            passenger.FirstName = data;
            passenger.LastName = data;
            passenger.SeatNumber = data;
            passenger.Email = data;
            passenger.PhoneNumber = data;
            passenger.Address = data;
            passenger.City = data;
            passenger.State = data;
            passenger.ZipCode = id;
            passenger.IsCheckedIn = true;
            passenger.IsMinor = true;
            passenger.IsSpecialNeeds = false;
            passenger.Active = true;
            return passenger;
           
        }

        [Test]
        public void TestAddPassenger()
        {
            Passenger passenger = new Passenger();
            passenger = createPassenger(100, "testing");
            int expect = 1;
            int actual = _passengerManager.addPassenger(passenger);
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestGetAllPassenger()
        {
            int expect = 10;
            int actual = _passengerManager.getAllPassengers().Count();
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestUpdatePassenger()
        {
            passengers[0].Active = false;
            int expect = 1;
            int actual = _passengerManager.updatePassenger(passengers[0]);
            Assert.That(actual, Is.EqualTo(expect));
        }
    }
}