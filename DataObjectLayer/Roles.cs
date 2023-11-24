namespace DataObjects
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RoleType { get; set; }



        public class RoleVM : Roles
        {
            public List<string> Roles { get; set; }
        }


    }
}
