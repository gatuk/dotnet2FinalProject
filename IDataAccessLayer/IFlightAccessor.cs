using DataObjects;

namespace DataAccessInterfaces
{
	public interface IFlightAccessor
	{
		List<Flight> SelectFlightsByDepartureDate();
		List<Flight> SelectFlightsByArrivalDate();
		List<Flight> SelectFlightsByDepartureCity();
		List<Flight> SelectFlightsByArrivalCity();
		List<Flight> SelectFlightsByDepartureTime();
		List<Flight> SelectFlightsByArrivalTime();
		List<Flight> SelectFlightsByAirline();
		List<Flight> SelectFlightsByFlightNumber();
		List<Flight> SelectFlightsByAirplaneID();
	}
}
