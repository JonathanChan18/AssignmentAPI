using System.Data;
using System.Data.SqlClient;
using DAL.Utils;
using common.Models;

namespace DAL.DAL
{
    public class EmployeeDAL
    {
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            //Create SQL Command
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Employee";
            cmd.Connection = dbConnection.connection;

            //Create a DataTable to store all of the values of the DB table
            DataTable dtb = new DataTable();

            //Fill the DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtb);

            //Create list of EmployeeModel objects
            List<EmployeeModel> list = new List<EmployeeModel>();

            //Iterate through each rows of the DataTable and assign each values of the employee properties to an employee object, then add to the list.
            foreach (DataRow row in dtb.Rows)
            {
                int employeeId = Convert.ToInt32(row["EmployeeId"]);
                string firstName = row["FirstName"].ToString();
                string lastName = row["LastName"].ToString();
                int age = Convert.ToInt32(row["Age"]);

                EmployeeModel employee = new EmployeeModel()
                {
                    EmployeeId = employeeId,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                list.Add(employee);
            }

        
            
            dbConnection.CloseConnection();
            return list;
        }

        public IEnumerable<EmployeeModel> AddEmployee(EmployeeModel addEmployee)
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            //Create SQL Command
            string insertQuery = "INSERT INTO Employee (FirstName,LastName,Age) VALUES (@FirstName,@LastName,@Age)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, dbConnection.connection))
            {
                // Add parameters and their values
                cmd.Parameters.AddWithValue("@FirstName", addEmployee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", addEmployee.LastName);
                cmd.Parameters.AddWithValue("@Age", addEmployee.Age);

                // Execute the command
                cmd.ExecuteNonQuery();

            }
            dbConnection.CloseConnection();
            return GetAllEmployees();
        }

        public IEnumerable<EmployeeModel> UpdateEmployee(EmployeeModel updateEmployee)
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            string updateQuery = "UPDATE Employee SET FirstName=@FirstName,LastName=@LastName,Age=@Age WHERE EmployeeId=@EmployeeId";

            using (SqlCommand cmd = new SqlCommand(updateQuery, dbConnection.connection))
            {
                // Add parameters and their values
                cmd.Parameters.AddWithValue("@EmployeeId", updateEmployee.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName", updateEmployee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", updateEmployee.LastName);
                cmd.Parameters.AddWithValue("@Age", updateEmployee.Age);

                // Execute the command
                cmd.ExecuteNonQuery();

            }
            dbConnection.CloseConnection();
            return GetAllEmployees();
        }

        public IEnumerable<EmployeeModel> DeleteEmployee(int employeeId)
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            EmployeeModel DeleteEmployee = new EmployeeModel()
            {
                EmployeeId = employeeId
            };

            string deleteQuery = "DELETE FROM Employee WHERE EmployeeId=@EmployeeId";

            using (SqlCommand cmd = new SqlCommand(deleteQuery, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", DeleteEmployee.EmployeeId);

                // Execute the command
                cmd.ExecuteNonQuery();
            }
            dbConnection.CloseConnection();
            return GetAllEmployees();
        }
    }
}
