using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTests
    {
        private IBankManager _Manager;
        private IBank _Bank;

        [TestInitialize]
        public void TestInit()
        {
            _Bank = Bank.Open("ABC");
            _Manager = new BankManager("Manager Joe", _Bank);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateCustomerException))]
        public void Bank_CanAddCustomerTest()
        {
           ICustomer _customer = _Bank.AddCustomer("224-81-7701", "Joe Customer");
           Assert.AreEqual("224-81-7701", _customer.SocialSecurityNumber);

            // Duplicate customer
          _Bank.AddCustomer(_customer);
        }

        [TestMethod]
        public void Bank_TotalInterestPaid()
        {
          


        }


    }
}
