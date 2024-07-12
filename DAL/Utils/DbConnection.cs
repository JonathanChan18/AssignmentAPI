using System.Data.SqlClient;

namespace DAL.Utils
{
    public class DbConnection
    {
        private string connString = "Data Source=localhost;Initial Catalog=EmployeeDB;Integrated Security=True";
        public SqlConnection connection;

        public DbConnection()
        {
            connection = new SqlConnection(connString);
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
