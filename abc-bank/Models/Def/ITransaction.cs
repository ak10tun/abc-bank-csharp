using abc_bank.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Models
{
    public interface ITransaction
    {
        TransactionType Type { get; }
        decimal Amount { get; }
        DateTime Date { get; }
    }
}
