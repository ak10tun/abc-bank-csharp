using System.Collections.Generic;

namespace abc_bank.Draft.Models
{
    public interface ICustomer
    {
        IEnumerable<IAccount> Accounts { get; }
        string SocialSecurityNumber { get; }
        int OpenAccount(IAccount account);
        IAccount OpenAccount(AccountType type, decimal initialDeposit);
        IAccount OpenCheckingAccount(decimal initialDeposit);
        IAccount OpenMaxiSavingsAccount(decimal initialDeposit);
        IAccount OpenSavingsAccount(decimal initialDeposit);
    }
}