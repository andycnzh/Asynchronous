using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace FaviconBrowser.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            TaskTest t = new TaskTest();

            var result = await t.TaskMethod();

            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
