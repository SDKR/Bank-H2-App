using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_Case_Bank
{
    interface IDBTransactions
    {

        List<Transaction> getTransactions(Account acc);

    }
}
