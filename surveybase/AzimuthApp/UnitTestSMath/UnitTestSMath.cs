using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestSMath
{
    [TestClass]
    public class UnitTestSMath
    {
        [TestMethod]
        public void TestDMStoDMS()
        {
            ZXY.SMath.DMStoDMS(1.4, out int d, out int m, out double s);
            Assert.AreEqual(1, d);
            Assert.AreEqual(40, m);
            Assert.AreEqual(0, s, 1e-8);

            ZXY.SMath.DMStoDMS(-1.4, out d, out m, out s);
            Assert.AreEqual(-1, d);
            Assert.AreEqual(-40, m);
            Assert.AreEqual(0, s, 1e-8);

            ZXY.SMath.DMStoDMS(235.07492345, out d, out m, out s);
            Assert.AreEqual(235, d);
            Assert.AreEqual(7, m);
            Assert.AreEqual(49.2345, s, 1e-8);

            ZXY.SMath.DMStoDMS(-235.07492345, out d, out m, out s);
            Assert.AreEqual(-235, d);
            Assert.AreEqual(-7, m);
            Assert.AreEqual(-49.2345, s, 1e-8);
        }
    }
}
