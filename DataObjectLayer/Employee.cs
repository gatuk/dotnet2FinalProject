namespace DataObjects
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
    }
    public class EmployeeVM : Employee
    {
        public List<string> Roles { get; set; }
    }

}
