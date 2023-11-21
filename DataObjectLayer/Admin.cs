namespace DataObjects
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AdminVM : Admin
    {
        public List<string> Roles { get; set; }
    }

}
