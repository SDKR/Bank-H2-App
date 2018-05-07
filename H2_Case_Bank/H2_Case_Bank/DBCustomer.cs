using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace H2_Case_Bank
{
    class DBCustomer
    {
        /*
         * 
         */

        public List<Customer> returnCustomers()
        {
            List<Customer> CusList = new List<Customer>();

            SqlConnection sqlConn = new SqlConnection(DatabaseLogin.constring);
            SqlCommand cmd = new SqlCommand("Select * from Customer", sqlConn);
            sqlConn.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            sqlConn.Close();

            var empList = ds.Tables[0].AsEnumerable().Select(dataRow => new Customer { UserID = dataRow.Field<int>("PK_ID"), Firstname = dataRow.Field<string>("FirstName"), Lastname = dataRow.Field<string>("LastName"), CreationDate = dataRow.Field<DateTime>("CreationDate") }).ToList();

            // Debug
            /*
            for (int i = 0; i < empList.Count; i++)
            {
                Console.WriteLine(empList[i].UserID);
                Console.WriteLine(empList[i].Firstname);
                Console.WriteLine(empList[i].Lastname);
                Console.WriteLine(empList[i].CreationDate);
                Console.WriteLine();
            }
            */
            //Customer Cus = new Customer(ds.Tables[0].Rows[0], );

            return empList;
        }

        /*
         * Create Customer, takes firstname, lastname
         */

        public void createCustomer(String Firstname, String Lastname)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseLogin.constring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    
                    // Creates todays date for fussy Mr. database
                    String formatsdate = @"MM\/dd\/yyyy HH:mm";
                    //DateTime thisDate = new DateTime();
                    DateTime localDate = DateTime.Now;
                    var cultureInfo = new CultureInfo("fr-FR");
                    string today = localDate.ToString(formatsdate);

                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Customer (FirstName, LastName, CreationDate) VALUES (@FirstName, @LastName, @CreationDate)";
                    command.Parameters.AddWithValue("@FirstName", Firstname);
                    command.Parameters.AddWithValue("@LastName", Lastname);
                    command.Parameters.AddWithValue("@CreationDate", today);
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        // error here
                        Console.WriteLine("CreateCustomer Error");
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        /*
         * Delete customer with userid x from list
         */

        public void deleteCustomer(int UserID)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseLogin.constring))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE PK_ID = " + UserID, connection))
                {
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        // error here
                        Console.WriteLine("Create account Error");
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            
        }

    }
}
