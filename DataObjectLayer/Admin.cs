namespace DataObjects
{
    public class Admin
    {
        /*
          * storage model for the Employee table 
         */
        public int AdminID { get; set; }
        public string? UserName { get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? Active { get; set; }

    }

    public class AdminVM : Admin
    {
        public List<string>? Roles { get; set; }

    }

}
