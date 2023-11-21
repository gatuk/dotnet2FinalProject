using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    public class FlightManagerTests
    {
        IFlightManager _flightManager = null;
        [TestInitialize]
        public void TestSetUp()
        {
            _flightManager = new FlightManager(new FlightAccessorFake());
        }
    }
}
