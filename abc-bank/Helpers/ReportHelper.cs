using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace abc_bank.Helpers
{
    public static class ReportHelper
    {
       
        public static string CustomerAccounts(string customerName, int numberOfAccounts)
        {
            ValidationHelper.StringNotNullEmpty(customerName, "customerName");
            return "\n - " + customerName + " (" + formatStringPlurality(numberOfAccounts, "account") + ")";
        }


        private static string formatStringPlurality(int number, String word)
        {
            ValidationHelper.StringNotNullEmpty(word, "word");
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
