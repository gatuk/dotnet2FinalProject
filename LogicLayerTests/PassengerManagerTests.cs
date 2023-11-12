using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting
namespace LogicLayerTests
{
	[TestClass]
	public class PassengerManagerTests
	{

		IPassengerManager _passengerManager = null;

		[TestInitialize]
		public void TestSetUp()
		{
			_passengerManager = new PassengerManager(new _passengerAccessorFake());

		}


	}
}