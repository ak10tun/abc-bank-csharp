using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Draft.Models
{
    public class MaxiSavingsAccount: AccountBase
    {
        public MaxiSavingsAccount(decimal initialDeposit):base(initialDeposit)
        {
        }

        public DateTime StartDate()
        {
            return base._StartDate;
        }
    }
}
