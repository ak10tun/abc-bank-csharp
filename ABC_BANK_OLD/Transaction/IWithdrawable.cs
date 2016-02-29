using abc_bank.Models;
using System;

namespace abc_bank.Draft
{
    public interface IWithdrawable
    {
        void Withdraw(decimal amount, DateTime date);

        void Withdraw(decimal amount);
        
    }
}