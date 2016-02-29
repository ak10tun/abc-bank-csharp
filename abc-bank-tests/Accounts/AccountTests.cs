using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests.Accounts
{
    [TestClass]
    public class AccountTests
    {
        IInterestAccount _SavingsAccount;
        IInterestAccount _CheckingAccount;
        IInterestAccount _MaxiSavingsAccount;

        [TestInitialize]
        public void TestInit()
        {
            // Initial deposit of $100
            // Do not override the default rates
            // Compound type is daily
            _SavingsAccount = new SavingsAccount(1000);
            _CheckingAccount = new CheckingAccount(1000);
            _MaxiSavingsAccount = new MaxiSavingsAccount(1000);

        }

        [TestMethod]
        public void Accounts_CanGetAccountsRegistry()
        {
            Assert.AreEqual(InterestAccountType.Savings, _SavingsAccount.GetAccountType());
            Assert.AreEqual(InterestAccountType.Checking, _CheckingAccount.GetAccountType());
            Assert.AreEqual(InterestAccountType.MaxiSavings, _MaxiSavingsAccount.GetAccountType());

            int _savingsTransactionsCount = _SavingsAccount.Transactions.Count;
            Assert.AreEqual(1, _savingsTransactionsCount);
            Assert.AreEqual(TransactionType.InitialDeposit, _SavingsAccount.Transactions[0].Type);
            Assert.AreEqual(DateTime.Now.Date, _SavingsAccount.StartDate.Date);
            Assert.AreEqual(1000, _SavingsAccount.GetBalance());
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void Accounts_AreTransactionsRegistered()
        {
            _SavingsAccount.Deposit(2000); //Tr 2
            Assert.AreEqual(3000, _SavingsAccount.GetBalance());
            Assert.AreEqual(0.002, _SavingsAccount.GetEffectiveRate());

            _SavingsAccount.Withdraw(70000); //Tr Expected Fail

            _SavingsAccount.Withdraw(1500); // Tr 3
            Assert.AreEqual(1500, _SavingsAccount.GetBalance());

            _SavingsAccount.Deposit(10000); // Tr 4
            _SavingsAccount.Transfer(_CheckingAccount, 4000); // Tr 5

            Assert.AreEqual(7500, _SavingsAccount.GetBalance());
            Assert.AreEqual(5000, _CheckingAccount.GetBalance());

            CoreList<ITransaction> _savingsTransactions = _SavingsAccount.Transactions;
            CoreList<ITransaction> _checkingTransactions = _SavingsAccount.Transactions;


            Assert.AreEqual(5, _savingsTransactions.Count);
            Assert.AreEqual(2, _checkingTransactions.Count);
        }

        [TestMethod]
        public void Accounts_AreInterestsAccruing()
        {
            DateTime _futDate_4 = DateProvider.Now().AddDays(4);
            DateTime _futDate_7 = DateProvider.Now().AddDays(7);


          

        }
    }
}
