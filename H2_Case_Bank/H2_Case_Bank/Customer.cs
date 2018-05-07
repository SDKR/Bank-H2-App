using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_Case_Bank
{
    class Customer
    {

        DBCustomer DBC = new DBCustomer();

        public int UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime CreationDate { get; set; }

        public void CreateCustomer(string firstname, string lastname)
        {
            DBC.createCustomer(firstname, lastname);
        }
        public void DeleteCustomer(int UserID)
        {
            DBC.deleteCustomer(UserID);
        }

            //Delete Customer

        public List<Customer> ReturnCustomers()
        {
            List<Customer> cslist = DBC.returnCustomers();
            return cslist;
        }
    }
}
