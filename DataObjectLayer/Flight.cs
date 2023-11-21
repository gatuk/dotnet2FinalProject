namespace DataObjects
{
<<<<<<< HEAD
    public class Flight
    {
=======
	public class Flight
	{
		public int FlightId { get; set; }
		public string? FlightNumber { get; set; }
		public string? Departure { get; set; }
		public string? Destination { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		//public int Capacity { get; set; }
		public int AvailableSeats { get; set; }
		public decimal Price { get; set; }
		public string Airline { get; set; }
		//public string Aircraft { get; set; }
		//public string Status { get; set; }
		//public bool Active { get; set; }
>>>>>>> f7d1e3700ebeb0de8e832e2f824d784ae8b58a67

        /*
        * storage model for the flight table 
       */
        public int FlightId { get; set; }
        public string? FlightNumber { get; set; }
        public string? Departure { get; set; }
        public string? Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        //public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        public string Airline { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public int AirplaneID { get; set; }
        //public string Aircraft { get; set; }
        //public string Status { get; set; }
        //public bool Active { get; set; }

        public class FlightVM : Flight
        {
            public List<string> Flights { get; set; }
        }

    }
}
