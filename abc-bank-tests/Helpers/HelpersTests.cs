using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Helpers;

namespace abc_bank_tests.Helpers
{
    [TestClass]
    public class ValidationHelperTest
    {
        private TestContext _TestContext;
        private static long _Newid;

        [TestInitialize]
        public void TestInit()
        {
            _Newid = ValidationHelper.GetNewId();
        }

        [TestMethod]
        public void Helpers_IncrementingNewId_Test()
        {
            Assert.IsTrue(ValidationHelper.GetNewId() == _Newid + 1);
        }
    }
}
