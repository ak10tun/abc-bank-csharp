using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Utils;

namespace abc_bank.Draft.Models
{
    public class CheckingAccount : AccountBase
    {
        public CheckingAccount(decimal initialDeposit):base(initialDeposit)
        {
        }

        public DateTime StartDate()
        {
            return base._StartDate;
        }
    }
}
