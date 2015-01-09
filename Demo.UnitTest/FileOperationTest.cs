using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Services;

namespace Demo.UnitTest
{
    [TestClass]
    public class FileOperationTest
    {
        [TestMethod]
        public void TestWrite()
        {
            new FileOperation().Write();
        }
    }
}
