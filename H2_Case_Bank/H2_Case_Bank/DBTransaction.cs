using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_Case_Bank
{
    class DBTransaction
    {
        
        public List<Transaction> getTransactions(Account acc)
        {
            /*
             * Pull transactions from database and return list 
             */
            List<Transaction> CusList = new List<Transaction>();

            SqlConnection sqlConn = new SqlConnection(DatabaseLogin.constring);
            SqlCommand cmd = new SqlCommand("Select * from Transactions where FK_AccountID = " + acc.Accountnumber + "", sqlConn);
            sqlConn.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            sqlConn.Close();

            var empList = ds.Tables[0].AsEnumerable().Select(dataRow => new Transaction
            {
                TransactionID = dataRow.Field<int>("PK_TransactionID"),
                Date = dataRow.Field<DateTime>("TransactionDate"),
                Amount = dataRow.Field<decimal>("Amount"),
                FromAccount = dataRow.Field<int>("FK_AccountID"),
            }).ToList();

            //Debug
            /*
            for (int i = 0; i < empList.Count; i++)
            {
                Console.WriteLine(empList[i].TransactionID);
                Console.WriteLine(empList[i].Date);
                Console.WriteLine(empList[i].Amount);
                Console.WriteLine(empList[i].FromAccount);
                Console.WriteLine();
            }*/

            return empList;
        }

    }
}
