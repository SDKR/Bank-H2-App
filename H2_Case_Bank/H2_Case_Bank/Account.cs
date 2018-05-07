using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_Case_Bank
{
    class Account : Customer
    {

        DBAccount DBA = new DBAccount();

        public int Accountnumber { get; set; }
        public string Accounttype { get; set; }
        public decimal Interest { get; set; }
        public decimal Balance { get; set; }
        public DateTime AccountCreation { get; set; }
        public int FK_CustomerID { get; set; }

        public void CreateAccount(Account acc)
        {
            DBA.createAccount(acc);
        }

        public void deleteAccount(Account acc)
        {
            DBA.deleteAccount(acc);
        }

        public List<Account> getCustomerAccounts(Customer cus)
        {
            return DBA.getAccounts(cus.UserID);
        }


        public void Deposit(int accountnumber, decimal transaction)
        {
            DBA.deposit(accountnumber, transaction);
        }

        public void Withdraw(int accountnumber, decimal transaction)
        {
            DBA.Withdraw(accountnumber, transaction);
        }

    }
}
