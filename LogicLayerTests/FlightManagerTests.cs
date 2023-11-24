using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;
namespace LogicLayerTests
{
    [TestClass]
    public class FlightManagerTests
    {
        private FlightManager _flightManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _flightManager = new FlightManager(new FlightAccessorFake());
        }
        [TestMethod]
        public void AddNewFlight_ValidFlight_ReturnsFlightId()
        {
            // Arrange: Create a valid flight object
            var flight = new Flight
            {
                // Set properties as needed for a valid flight
                FlightNumber = "ABC123",
                Departure = "New York",
                Destination = "Los Angeles",
                DepartureTime = DateTime.Now.AddHours(2),
                ArrivalTime = DateTime.Now.AddHours(5)
            };
            //Act: Call the method you want to test
            var flightId = _flightManager.addNewFlight(flight);
            //Assert
            // Assert: Check if the returned flightId is greater than 0, indicating success
            Assert.IsTrue(flightId > 0);
        }
        [TestMethod]
        public void AddNewFlight_InvalidFlight_ReturnsZero()
        {
            // Arrange: Create an invalid flight object (e.g., missing required fields or invalid values)
            var invalidFlight = new Flight
            {
                // Omitting required fields or setting them to invalid values
                FlightNumber = null, // Omitting a required field
                Departure = "New York",
                Destination = null, // Omitting another required field
                DepartureTime = DateTime.Now.AddHours(2),
                ArrivalTime = DateTime.Now.AddHours(1) // Arrival time before departure time
            };

            // Act: Call the method you want to test
            var flightId = _flightManager.addNewFlight(invalidFlight);

            // Assert: Check if the returned flightId is 0, indicating failure
            Assert.That(flightId, Is.EqualTo(0));
        }

        [TestMethod]
        public void EditFlight_ValidFlight_ReturnsSuccess()
        {
            // Arrange: Create a valid flight object and add it to the data source first

            var validFlight = new Flight
            {
                FlightNumber = "XYZ789",
                Departure = "Chicago",
                Destination = "Miami",
                DepartureTime = DateTime.Now.AddHours(2),
                ArrivalTime = DateTime.Now.AddHours(5)
            };
            var flightId = _flightManager.addNewFlight(validFlight);

            // Modify the flight data
            validFlight.Destination = "Dallas";

            // Act: Call the method you want to test
            var result = _flightManager.editFlight(validFlight);

            // Assert: Check if the edit operation was successful (e.g., it returned a positive value)
            Assert.IsTrue(result > 0);

        }
        [TestMethod]
        public void EditFlight_InvalidFlight_ReturnsFailure()
        {
            // Arrange: Create an invalid flight object (e.g., missing required fields)
            var invalidFlight = new Flight
            {
                // Omitting required fields or setting them to invalid values
                FlightNumber = null, // Omitting a required field
                Departure = "New York",
                Destination = "Los Angeles",
                DepartureTime = DateTime.Now.AddHours(2),
                ArrivalTime = DateTime.Now.AddHours(1) // Arrival time before departure time

            };

            // Act: Call the method you want to test
            var result = _flightManager.editFlight(new Flight());

            // Assert: Check if the edit operation failed (e.g., it returned a non-positive value)
            Assert.IsTrue(result <= 0);
        }
        [TestMethod]
        public void GetAllAirPortCodes_ReturnsListOfCodes()
        {
            // Arrange: Create a list of expected airport codes
            var expectedAirportCodes = new List<string> { "LAX", "JFK", "ORD", "DFW", "MIA" };

            // Act: Call the method you want to test
            var actualAirportCodes = _flightManager.getAllAirPortCodes();

            // Assert: Check if the result is a non-null list
            Assert.IsNotNull(actualAirportCodes);

            // Assert: Check if the result contains the expected airport codes
            CollectionAssert.AreEqual(expectedAirportCodes, actualAirportCodes);

        }
        [TestMethod]
        public void GetAllFlights_ReturnsListOfFlights()
        {
            // Act: Call the method you want to test
            var actualFlights = _flightManager.getAllFlights();

            // Assert: Check if the result is a non-null list
            Assert.IsNotNull(actualFlights);

            // Assert: Check if the result contains at least one flight
            Assert.IsTrue(actualFlights.Count > 0);

            // Check if all departure times are in the future
            foreach (var flight in actualFlights)
            {
                Assert.IsTrue(flight.DepartureTime > DateTime.Now);
            }

            // Check if flight numbers are unique within the result
            CollectionAssert.AllItemsAreUnique(actualFlights.Select(f => f.FlightNumber).ToList());
        }


    }
}

