//------------------------------------------------------------
// Copyright (c) 2017 AndyCnZh.  All rights reserved.
//------------------------------------------------------------

namespace FaviconBrowser.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            // MSTest support async/await
            // The test method must return Task, 
            // if return void, then this test case can't be found in test explorer.
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
