using System.Data.SqlClient;

namespace DataAccessLayer
{
  internal static class SqlConnectionProvider
  {
    // connections strings always include server, database, credentials
    public static string ConnectionString = @"Data Source=localhost; Initial Catalog=Northwind;Integrated Security=True";

    public static SqlConnection GetConnection()
    {
            SqlConnection connection = new SqlConnection(ConnectionString);
      return connection;
    }
  }

}
