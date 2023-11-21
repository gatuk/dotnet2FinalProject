using DataObjects;
namespace LogicLayerInterfaces
{
    public interface IEmployeeManager
    {
        int verifyUser(string username, string password);

        //gishe added code below here
        EmployeeVM LoginEmployee(string email, string password);
        // helper methods, but public for reuse
        string HashSha256(string source);
        bool AuthenticateEmployee(string email, string password);
        EmployeeVM GetEmployeeVMByEmail(string email);
        List<string> GetRolesByEmployeeID(int employeeID);
        bool ResetPassword(string email, string oldPassword, string newPassword);

    }
}
