namespace DataObjects
{
  public class Passenger
  {
    public int PassengerID { get; set; }
    public int FlightID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SeatNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int ZipCode { get; set; }
    public bool IsCheckedIn { get; set; }
    public bool IsMinor { get; set; }
    public bool IsSpecialNeeds { get; set; }
    public bool Active { get; set; }
  }

  public class PassengerVM : Passenger
  {
    public List<string> Roles { get; set; }
  }
}
