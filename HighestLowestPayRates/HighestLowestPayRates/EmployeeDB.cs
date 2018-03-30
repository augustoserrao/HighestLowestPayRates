/* Augusto Serrao
   Deepti Aggarwal
   Hartaj Dhillon
   Gagandeep Singh
   Shoaib Hassan
*/

using System.Collections.Generic;
using System.Data.SqlClient;

namespace HighestLowestPayRates
{
    class EmployeeDB
    {
        const string CONNECT_STRING = @"Server=.\SQLEXPRESS;Database=Personnel;Trusted_Connection=True;";
        SqlConnection conn;

        static EmployeeDB employeeDB;

        private EmployeeDB()
        {
            conn = new SqlConnection(CONNECT_STRING);
            conn.Open();

            string cmdString = "IF OBJECT_ID('dbo.Employee', 'U') IS NULL " +
                                    "CREATE TABLE Employee ( " +
                                        "ID INT NOT NULL " +
                                            "CONSTRAINT employee_PK PRIMARY KEY, " +
                                        "FirstName VARCHAR(20) NOT NULL, " +
                                        "LastName VARCHAR(20) NOT NULL, " +
                                        "Position VARCHAR(50) NOT NULL, " +
                                        "HourlyPayRate DECIMAL(15,2) NOT NULL, " +
                                        "Age INT NOT NULL, " +
                                        "ImagePath VARCHAR(50) NOT NULL) ";

            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.ExecuteNonQuery();
        }

        public static EmployeeDB GetInstance()
        {
            if (employeeDB == null)
                employeeDB = new EmployeeDB();
            return employeeDB;
        }


        public void Add(Employee employee)
        {
            string cmdString = "INSERT INTO Employee " +
                                    "(ID, FirstName, LastName, Position, HourlyPayRate, Age, ImagePath)" +
                                    "VALUES" +
                                    "(@ID, @FIRSTNAME, @LASTNAME, @POSITION, @HOURLYPAYRATE, @AGE, @IMAGEPATH)";

            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@ID", employee.ID);
            cmd.Parameters.AddWithValue("@FIRSTNAME", employee.FirstName);
            cmd.Parameters.AddWithValue("@LASTNAME", employee.LastName);
            cmd.Parameters.AddWithValue("@POSITION", employee.Position);
            cmd.Parameters.AddWithValue("@HOURLYPAYRATE", employee.HourlyPayRate);
            cmd.Parameters.AddWithValue("@AGE", employee.Age);
            cmd.Parameters.AddWithValue("@IMAGEPATH", employee.ImagePath);

            cmd.ExecuteNonQuery();
        }

        public void Delete(Employee employee)
        {
            string cmdString = "DELETE Employee " +
                               "WHERE ID = @ID";

            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@ID", employee.ID);

            cmd.ExecuteNonQuery();
        }

        public void Update(Employee employee)
        {
            string cmdString = "UPDATE Employee SET FirstName = @FIRSTNAME, " +
                                                    "LastName = @LASTNAME, " +
                                                    "HourlyPayRate = @HOURLYPAYRATE, " +
                                                    "Age = @AGE, " +
                                                    "ImagePath = @IMAGEPATH " +
                                "WHERE ID = @ID";

            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@ID", employee.ID);
            cmd.Parameters.AddWithValue("@FIRSTNAME", employee.FirstName);
            cmd.Parameters.AddWithValue("@LASTNAME", employee.LastName);
            cmd.Parameters.AddWithValue("@POSITION", employee.Position);
            cmd.Parameters.AddWithValue("@HOURLYPAYRATE", employee.HourlyPayRate);
            cmd.Parameters.AddWithValue("@AGE", employee.Age);
            cmd.Parameters.AddWithValue("@IMAGEPATH", employee.ImagePath);

            cmd.ExecuteNonQuery();
        }

        public List<Employee> Get()
        {
            List<Employee> employeeList = new List<Employee>();
            string cmdString = "SELECT ID, FirstName, LastName, Position, HourlyPayRate, Age, ImagePath" +
                               " FROM Employee";
            SqlCommand cmd = new SqlCommand(cmdString, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
                employeeList.Add(new Employee()
                {
                    ID = rd.GetInt32(rd.GetOrdinal("ID")),
                    FirstName = rd.GetString(rd.GetOrdinal("FirstName")),
                    LastName = rd.GetString(rd.GetOrdinal("LastName")),
                    Position = rd.GetString(rd.GetOrdinal("Position")),
                    HourlyPayRate = rd.GetDecimal(rd.GetOrdinal("HourlyPayRate")),
                    Age = rd.GetInt32(rd.GetOrdinal("Age")),
                    ImagePath = rd.GetString(rd.GetOrdinal("ImagePath")),
                });
            rd.Close();

            return employeeList;
        }
    }
}
