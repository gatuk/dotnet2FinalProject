using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEmployeeAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        EmployeeVM SelectEmployeeVMByEmail(string email);
        List<string> SelectRolesByEmployee(int employeeID);
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
        public int verifyUser(string username, string password);

    }

}
