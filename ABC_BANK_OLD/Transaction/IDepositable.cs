using abc_bank.Models;
using System;

namespace abc_bank.Draft
{
    public interface IDepositable
    {
        void Deposit(decimal amount);
        void Deposit(decimal amount, DateTime date);
   
    }
}