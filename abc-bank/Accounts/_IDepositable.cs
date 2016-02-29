using System;

namespace abc_bank
{
    public interface IDepositable
    {
        void Deposit(decimal amount);
        void Deposit(decimal amount, DateTime date);
   
    }
}