using DataAccessInterfaces;
using DataObjects;
using static DataObjects.Flight;

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
        public List<Flight> SelectFlightsByDepartureDate()
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public List<Flight> SelectFlightsByArrivalDate()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByDepartureCity()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByArrivalCity()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByDepartureTime()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByArrivalTime()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByAirline()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByFlightNumber()
        {
            throw new NotImplementedException();
        }

        public List<Flight> SelectFlightsByAirplaneID()
        {
            //throw new NotImplementedException();
            foreach (var item in fakeFlights)
            {
                item.Flights = new List<string>();
                item.Flights.Add(item.FlightNumber);
                item.Flights.Add(item.Departure);
                item.Flights.Add(item.Destination);
                item.Flights.Add(item.DepartureTime.ToString());
                item.Flights.Add(item.ArrivalTime.ToString());
                item.Flights.Add(item.AvailableSeats.ToString());
                item.Flights.Add(item.Price.ToString());
                item.Flights.Add(item.Airline);
                item.Flights.Add(item.DepartureDate.ToString());
                item.Flights.Add(item.ArrivalDate.ToString());
                item.Flights.Add(item.DepartureCity);
                item.Flights.Add(item.ArrivalCity);
                item.Flights.Add(item.AirplaneID.ToString());
                item.Flights.Add(item.FlightId.ToString());
            }
            //return fakeFlights;
            throw new NotImplementedException();
        }

        public List<Flight> selectAllFlights()
        {
            //throw new NotImplementedException();
            foreach (var item in fakeFlights)
            {
                item.Flights = new List<string>();
                item.Flights.Add(item.FlightNumber);
                item.Flights.Add(item.Departure);
                item.Flights.Add(item.Destination);
                item.Flights.Add(item.DepartureTime.ToString());
                item.Flights.Add(item.ArrivalTime.ToString());
                item.Flights.Add(item.AvailableSeats.ToString());
                item.Flights.Add(item.Price.ToString());
                item.Flights.Add(item.Airline);
                item.Flights.Add(item.DepartureDate.ToString());
                item.Flights.Add(item.ArrivalDate.ToString());
                item.Flights.Add(item.DepartureCity);
                item.Flights.Add(item.ArrivalCity);
                item.Flights.Add(item.AirplaneID.ToString());
                item.Flights.Add(item.FlightId.ToString());
            }
            //return fakeFlights;
            throw new NotImplementedException();


        }

        public int insert(Flight flight)
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
=======
        public int insert(Flight flight)
        {
            throw new NotImplementedException();
>>>>>>> f7d1e3700ebeb0de8e832e2f824d784ae8b58a67
        }

        public List<string> selectAllAirportCode()
        {
<<<<<<< HEAD
            //throw new NotImplementedException();
            List<string> airportCodes = new List<string>();
            airportCodes.Add("BOI");
            airportCodes.Add("SEA");
            airportCodes.Add("LAX");
            airportCodes.Add("SFO");
            airportCodes.Add("DEN");
            airportCodes.Add("SLC");
            airportCodes.Add("LAS");
            airportCodes.Add("PHX");
            airportCodes.Add("PDX");
            airportCodes.Add("MSP");
            airportCodes.Add("ATL");
            airportCodes.Add("ORD");
            airportCodes.Add("DFW");
            airportCodes.Add("IAH");
            airportCodes.Add("JFK");
            airportCodes.Add("MIA");
            airportCodes.Add("BOS");
            // return the list of airport codes
            return airportCodes;
=======
            throw new NotImplementedException();
>>>>>>> f7d1e3700ebeb0de8e832e2f824d784ae8b58a67
        }

        public int updateFlight(Flight flight)
        {
<<<<<<< HEAD
            //throw new NotImplementedException();
            int result = 0;
            foreach (var item in fakeFlights)
            {
                if (item.FlightId == flight.FlightId)
                {
                    item.FlightNumber = flight.FlightNumber;
                    item.Departure = flight.Departure;
                    item.Destination = flight.Destination;
                    item.DepartureTime = flight.DepartureTime;
                    item.ArrivalTime = flight.ArrivalTime;
                    item.AvailableSeats = flight.AvailableSeats;
                    item.Price = flight.Price;
                    item.Airline = flight.Airline;
                    item.DepartureDate = flight.DepartureDate;
                    item.ArrivalDate = flight.ArrivalDate;
                    item.DepartureCity = flight.DepartureCity;
                    item.ArrivalCity = flight.ArrivalCity;
                    item.AirplaneID = flight.AirplaneID;
                    //item.Flights = flight.Flights;
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

=======
            throw new NotImplementedException();
>>>>>>> f7d1e3700ebeb0de8e832e2f824d784ae8b58a67
        }
    }
}
