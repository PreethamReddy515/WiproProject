using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDemo2
{
    [TestFixture]
    public class CalculationTest
    {
        [Test]
        public void TestSum()
        {

            Mock<Calculation> mockObject = new Mock<Calculation>();
            mockObject.Setup(x => x.Sum(12, 5)).Returns(17);
            Assert.AreEqual(17, mockObject.Object.Sum(12, 5));
        }

        [Test]
        public void TestSub()
        {
            Mock<Calculation> mockObject = new Mock<Calculation>();
            mockObject.Setup(x => x.Sub(12, 5)).Returns(7);
            Assert.AreEqual(7, mockObject.Object.Sub(12, 5));
        }

        [Test]
        public void TestMult()
        {
            Mock<Calculation> mockObject = new Mock<Calculation>();
            mockObject.Setup(x => x.Mult(12, 5)).Returns(60);
            Assert.AreEqual(60, mockObject.Object.Mult(12, 5));
        }
    }
}
