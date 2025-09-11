using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractTraining.EmployAbstractClass;

namespace AbstractTraining
{
    internal class Employ1 : Employ
    {
        public Employ1(int empno, string name, double basic) : base(empno, name, basic)
        {

        }
    }
}
