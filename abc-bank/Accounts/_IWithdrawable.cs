using System;

namespace abc_bank
{
    public interface IWithdrawable
    {
        void Withdraw(decimal amount, DateTime date);

        void Withdraw(decimal amount);
        
    }
}