
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractTraining.EmployAbstractClass;

namespace AbstractTraining
{
    internal class EmployMain
    {
        static void Main(string[] args)
        {
            Employ[] arrEmploy = new Employ[]
            {
                new Employ1(1, "emp1", 82344),
                new Employ2(2, "emp2",88411),
                new Employ3(3, "emp3",88114)
            };

            foreach (Employ employ in arrEmploy)
            {
                Console.WriteLine(employ);
            }
        }
    }
}
