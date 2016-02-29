using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
           IBank _Bank = Bank.Open("ABC");
           IBankManager _Manager = new BankManager("Manager Joe", _Bank);
           ICustomer _customer = _Bank.AddCustomer("224-81-7701", "Joe Customer");

        }
    }
}
