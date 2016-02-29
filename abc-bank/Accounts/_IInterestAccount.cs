using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IInterestAccount
    {
        long Id { get; }
        IList<IRateLimit> RateLimits { get; }
        DateTime StartDate { get;}
        double DefaultInterestRate { get; }
        //decimal AvailableBalance { get; }
        InterestAccountType Type { get; }
        string TypeName { get; }
        CoreList<MonetaryCycle> MonetaryCycles { get; }
        InterestAccountType GetAccountType();
        string GetStatement();
        decimal InterestEarned();
        decimal InterestEarned(DateTime date);
        decimal GetBalance();
        decimal GetBalance(DateTime date);
        double GetEffectiveRate();
        double GetEffectiveRate(DateTime date);
        CoreList<ITransaction> Transactions { get; }
        void Deposit(decimal amount);
        void Deposit(decimal amount, DateTime date);
        void Dispose();
        void Transfer(IInterestAccount receivingAccount, decimal amount);
        void Transfer(IInterestAccount receivingAccount, decimal amount, DateTime date);
        void Withdraw(decimal amount);
        void Withdraw(decimal amount, DateTime date);

    }
}
