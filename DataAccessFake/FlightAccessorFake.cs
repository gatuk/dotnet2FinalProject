using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class FlightAccessorFake : IFlightAccessor
    {
        //gishe added this from jim's code
        // create a few fake flight for testing
        private List<FlightVM> fakeFlights = new List<FlightVM>();
        public FlightAccessorFake()
        {
            fakeFlights.Add(new FlightVM()
            {
                FlightId = 1,
                FlightNumber = "123",
                Departure = "Boise",
                Destination = "Seattle",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Today,
                AvailableSeats = 100,
                Price = 100,
                Airline = "Delta",
                DepartureDate = DateTime.Today,
                ArrivalDate = DateTime.Today,
                DepartureCity = "Boise",
                ArrivalCity = "Seattle",
                AirplaneID = 1,
                Flights = new List<string>()
            });
            fakeFlights.Add(new FlightVM()
            {
                FlightId = 2,
                FlightNumber = "456",
                Departure = "Boise",
                Destination = "Seattle",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Today,
                AvailableSeats = 100,
                Price = 100,
                Airline = "Delta",
                DepartureDate = DateTime.Today,
                ArrivalDate = DateTime.Today,
                DepartureCity = "Boise",
                ArrivalCity = "Seattle",
                AirplaneID = 1,
                Flights = new List<string>()
            });

        }
        public List<Flight> SelectFlightsByAirplaneID()
        {
            ////throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.AirplaneID == item.AirplaneID)
                {
                    flights.Add(item);
                }
            }
            return flights;

        }
        public List<Flight> selectAllFlights()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                flights.Add(item);
            }
            return flights;


        }
        public int Insert(Flight flight)
        {
            //throw new NotImplementedException();
            int result = 0;
            foreach (var item in fakeFlights)
            {
                if (item.FlightId == flight.FlightId)
                {
                    result = -1;
                }
            }
            if (result == 0)
            {
                fakeFlights.Add((FlightVM)flight);
                result = 1;
            }
            return result;
        }
        public List<string> selectAllAirportCode()
        {
            //throw new NotImplementedException();
            List<string> airports = new List<string>();
            foreach (var item in fakeFlights)
            {
                airports.Add(item.Departure);
                airports.Add(item.Destination);
            }
            return airports;
        }

        public int updateFlight(Flight flight)
        {
            //throw new NotImplementedException();
            int result = 0;
            foreach (var item in fakeFlights)
            {
                if (item.FlightId == flight.FlightId)
                {
                    fakeFlights.Remove(item);
                    fakeFlights.Add((FlightVM)flight);
                    result = 1;
                }
            }
            return result;


        }
        public int deleteFlight(Flight flight)
        {
            //throw new NotImplementedException();
            int result = 0;
            foreach (var item in fakeFlights)
            {
                if (item.FlightId == flight.FlightId)
                {
                    fakeFlights.Remove(item);
                    result = 1;
                }
            }
            return result;
        }
        public List<Flight> SelectFlightsByDepartureDate()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.DepartureDate == item.DepartureDate)
                {
                    flights.Add(item);
                }
            }
            return flights;
        }
        public List<Flight> SelectFlightsByArrivalDate()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.ArrivalDate == item.ArrivalDate)
                {
                    flights.Add(item);
                }
            }
            return flights;
        }

        public List<Flight> SelectFlightsByDepartureCity()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.DepartureCity == item.DepartureCity)
                {
                    flights.Add(item);
                }
            }
            return flights;
        }

        public List<Flight> SelectFlightsByArrivalCity()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.ArrivalCity == item.ArrivalCity)
                {
                    flights.Add(item);
                }
            }
            return flights;
        }

        public List<Flight> SelectFlightsByDepartureTime()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.DepartureTime == item.DepartureTime)
                {
                    flights.Add(item);
                }
            }
            return flights;
        }

        public List<Flight> SelectFlightsByArrivalTime()
        {
            //throw new NotImplementedException();
            List<Flight> flights = new List<Flight>();
            foreach (var item in fakeFlights)
            {
                if (item.ArrivalTime == item.ArrivalTime)
                {
                    flights.Add(item);
                }
            }
            return flights;
        }
        public List<Flight> SelectFlightsByAirline(string targetAirline)
        {
            List<Flight> flights = new List<Flight>();

            foreach (var item in fakeFlights)
            {
                if (item.Airline == targetAirline)
                {
                    flights.Add(item);
                }
            }

            return flights;
        }


        public List<Flight> SelectFlightsByFlightNumber(string targetFlightNumber)
        {
            List<Flight> flights = new List<Flight>();

            foreach (var item in fakeFlights)
            {
                if (item.FlightNumber == targetFlightNumber)
                {
                    flights.Add(item);
                }
            }

            return flights;
        }


    }
}
