using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockExample
{
    [TestFixture]
    internal class DetailsTest
    {
        [Test]
        public void TestShowStudent()
        {
            Mock<IDetails> mockDetails = new Mock<IDetails>();
            mockDetails.Setup(d => d.ShowStudent()).Returns("Hi I am not neha..");
            Assert.AreEqual("Hi I am not neha..",mockDetails.Object.ShowStudent());
        }
        [Test]
        public void TestShowCompany()
        {
            Mock<IDetails> mockDetails = new Mock<IDetails>();
            mockDetails.Setup(d => d.ShowCompany()).Returns("Its not from Wipro Hyderabad..");
            Assert.AreEqual("Its not from Wipro Hyderabad..", mockDetails.Object.ShowCompany());
        }
    }
}
