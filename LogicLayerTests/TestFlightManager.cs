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
            flights.Add(createFlight("flight1",1,100));
            flights.Add(createFlight("flight2", 2, 100));
            flights.Add(createFlight("flight3", 3, 100));
            flights.Add(createFlight("flight4", 4, 100));
            flights.Add(createFlight("flight5", 5, 100));
            flights.Add(createFlight("flight6", 6, 100));
            flights.Add(createFlight("flight7", 7, 100));
            flights.Add(createFlight("flight8", 8, 100));
            flights.Add(createFlight("flight9", 9, 100));
            flights.Add(createFlight("flight10", 10, 100));
            flightAccessor = new FakeFlightAccessor(flights);
            flightManager = new FlightManager(flightAccessor);
        }

        private Flight createFlight(string data, int seatNumber, int price)
        {
            Flight flight = new Flight();
            flight.FlightId = 1;
            flight.FlightNumber = data;
            flight.Departure = data;
            flight.Destination = data;
            flight.DepartureTime = DateTime.Now;
            flight.ArrivalTime = DateTime.Now.AddHours(1);
            flight.AvailableSeats = seatNumber;
            flight.Price = price;
            flight.Airline = data;
            return flight;
        }

        [Test]
        public void TestAddNewFlight()
        {
            Flight flight = new Flight();
            flight = createFlight("testing", 14, 200);
            int expect = 1;
            int actual = flightManager.addNewFlight(flight);
            Assert.That(actual, Is.EqualTo(expect));
        }

        [Test]
        public void TestDeleteFlight()
        {
            Flight flight = new Flight();
            flight = flights[0];
            int expect = 1;
            int actual = flightManager.deleteFlight(flight);
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestEditFlight()
        {
            Flight flight = new Flight();
            flight = flights[0];
            flight.ArrivalTime = DateTime.Now.AddHours(2);
            int expect = 1;
            int actual = flightManager.editFlight(flight);
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestGetAllAirCodes()
        {
            int expect = 10;
            int actual = flightManager.getAllAirPortCodes().Count;
            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void TestGetAllFlights()
        {
            int expect = 10;
            int actual = flightManager.getAllFlights().Count;
            Assert.That(actual, Is.EqualTo(expect));
        }
    }
}