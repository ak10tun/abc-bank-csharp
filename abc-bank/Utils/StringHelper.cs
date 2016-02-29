using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace abc_bank
{
    public static class StringHelper
    {
        // Also added to the extensions.
        public static String ToDollars(decimal d)
        {
            return String.Format("$%,.2f", Math.Abs(d));
        }

        public static string FormatStringPlurality(int number, String word)
        {
            ValidationHelper.StringNullEmpty(word, "word");
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
