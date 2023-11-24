using DataObjects;
namespace DataAccessInterfaces
{
    public interface IEmployeeAccessor
    {
        //from Jim's code
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        EmployeeVM SelectEmployeeVMByEmail(string email);
        List<string> SelectRolesByEmployee(int employeeID);
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
        //tutor added this method
        public int verifyUser(string username, string password);

    }

}
