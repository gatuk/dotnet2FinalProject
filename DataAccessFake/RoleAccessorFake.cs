using DataAccessInterfaces;
namespace DataAccessFakes
{
    public class RoleAccessorFake : IRoleAccessor
    {
        public List<string> selectRoles()
        {
            //throw new NotImplementedException();
            // not sure if this is the right way to do this
            List<string> roles = new List<string>();
            roles.Add("Admin");
            roles.Add("AirLineStaff");
            roles.Add("passenger");
            return roles;


        }
    }
}

