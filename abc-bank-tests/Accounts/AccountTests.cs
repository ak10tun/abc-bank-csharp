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
            IInterestAccount _SavingsAccount;
            IInterestAccount _CheckingAccount;
            IInterestAccount _MaxiSavingsAccount;

            // Initial deposit of $100
            // Do not override the default rates
            // Compound type is daily
            _SavingsAccount = new SavingsAccount(1000);
            _CheckingAccount = new CheckingAccount(1000);
            _MaxiSavingsAccount = new MaxiSavingsAccount(1000);

            _SavingsAccount.Deposit(2000); //Tr 2
            _SavingsAccount.Withdraw(1500); // Tr 3
            _SavingsAccount.Deposit(10000); // Tr 4
            _SavingsAccount.Transfer(_CheckingAccount, 4000); // Tr 5

            DateTime _futDate_4 = DateProvider.Now().AddDays(4);
            DateTime _futDate_7 = DateProvider.Now().AddDays(7);


            // No transactions prior to the periods
            decimal _fut4_interest;
            decimal _fut7_interest;

            _fut4_interest = _SavingsAccount.InterestEarned(_futDate_4);
            _fut7_interest = _SavingsAccount.InterestEarned(_futDate_7);
            

            Debug.Print(_fut4_interest.ToString());

            Assert.AreEqual("$0.16", _fut4_interest.ToDollars());
            Assert.AreEqual("$0.29", _fut7_interest.ToDollars());

            // Test again when there is transaction in duration


            DateTime _futDate_8 = DateProvider.Now().AddDays(8);
            DateTime _futDate_9 = DateProvider.Now().AddDays(9);
            DateTime _futDate_11 = DateProvider.Now().AddDays(11);

            _SavingsAccount.Deposit(5000, _futDate_8);
            _SavingsAccount.Withdraw(7000, _futDate_9);

            _fut7_interest = _SavingsAccount.InterestEarned(_futDate_11);
            Assert.AreEqual("$0.46", _fut7_interest.ToDollars());



        }
    }
}
