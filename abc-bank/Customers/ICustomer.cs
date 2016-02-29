using System.Collections.Generic;

namespace abc_bank
{
    public interface ICustomer
    {
        CoreList<IInterestAccount> InterestAccounts { get; }
        string GetName();
        string SocialSecurityNumber { get; }
        void OpenInterestAccount(IInterestAccount account);
        int GetNumberOfAccounts();
        string GetStatement();
        decimal TotalInterestEarned();
        IInterestAccount OpenInterestAccount(InterestAccountType type, decimal initialDeposit, InterestCounpoundType compoundType, double defaultRate);

    }
}