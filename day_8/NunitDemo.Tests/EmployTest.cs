using NunitDemos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitDemo.Tests
{
    [TestFixture]
    internal class EmployTest
    {
        [Test]
        public void TestSearchEmploy()
        {
            EmployDao employDao = new EmployDao();
            Employ employFound = employDao.SearchEmploy(1);
            Assert.IsNotNull(employFound);
            employFound = employDao.SearchEmploy(-1);
            Assert.IsNull(employFound);
        }
        [Test]
        public void TestShowEmploy()
        {
            List<Employ> employList = new EmployDao().ShowEmploy();
            Assert.AreEqual(4, employList.Count);
        }
        [Test]
        public void TestToString()
        {
            Employ employ = new Employ();
            employ.Empno = 1;
            employ.Name = "Neha";
            employ.Basic = 35000;
            string expected = "Empno 1 Name Neha Basic 35000";
            Assert.AreEqual(expected, employ.ToString());
        }
        [Test]
        public void TestGettersAndSetters()
        {
            Employ employ = new Employ();
            employ.Empno = 2;
            employ.Name = "AkuthotaNeha";
            employ.Basic = 45000;

            Assert.AreEqual(2, employ.Empno);
            Assert.AreEqual("AkuthotaNeha", employ.Name);
            Assert.AreEqual(45000, employ.Basic);
        }
        [Test]
        public void TestConstructor()
        {
            Employ employ = new Employ();
            Assert.NotNull(employ);
            Employ employ1 = new Employ(3, "ANeha", 55000);
            Assert.AreEqual(3, employ1.Empno);
            Assert.AreEqual("ANeha", employ1.Name);
            Assert.AreEqual(55000, employ1.Basic);
        }
    }
}
