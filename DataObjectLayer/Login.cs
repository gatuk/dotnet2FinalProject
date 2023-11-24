namespace DataObjects
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginVM : Login
    {
        public List<string> Logins { get; set; }
    }
}
