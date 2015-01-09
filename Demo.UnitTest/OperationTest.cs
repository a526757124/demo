using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Services;

namespace Demo.UnitTest
{
    [TestClass]
    public class OperationTest
    {
        [TestMethod]
        public void Add()
        {
            var op = new Operation();
            var result= op.Add(1, 2)>0;
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AddMax()
        {
            var op = new Operation();
            var result = op.Add(99999999, 2);
            //Assert.IsTrue(result < 0);
            Assert.IsTrue(result > 0);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))] 
        public void AddException()
        {
            var op = new Operation();
            var result = op.Add(999999999, 2);
            
        }
    }
}
