namespace DataObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserRole { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool Active { get; set; }
    }
    public class UserVM : User
    {
        public List<string> Users { get; set; }
    }
}
