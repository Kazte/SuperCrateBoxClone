using System;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCollision()
        {
            Collider col1 = new Collider(100, 100, 32, 32, 16, 16, true, false);
            Collider col2 = new Collider(100, 120, 32, 32, 16, 16, true, false);
            Assert.IsTrue(Collider.CheckCollision(col1, col2));
        }
    }
}
