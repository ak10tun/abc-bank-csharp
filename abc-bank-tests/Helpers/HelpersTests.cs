using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests.Helpers
{
    [TestClass]
    public class ValidationHelperTest
    {
        private static long _Newid;

        [TestInitialize]
        public void TestInit()
        {
            _Newid = IdentifierHelper.NewAccountId();
        }

        [TestMethod]
        public void Helpers_IncrementingNewId_Test()
        {
            Assert.IsTrue(IdentifierHelper.NewAccountId() == _Newid + 1);
            Assert.IsTrue(IdentifierHelper.NewAccountId() == _Newid + 2);
            Assert.IsFalse(IdentifierHelper.NewAccountId() == _Newid + 4);
        }

        [TestMethod]
        public void Helpers_SocialSecurityNumberTest()
        {
            string id = ValidationHelper.SocialSecurityNumber("224-81-7701");
            Assert.AreEqual("224-81-7701", id);
        }
    }
}
