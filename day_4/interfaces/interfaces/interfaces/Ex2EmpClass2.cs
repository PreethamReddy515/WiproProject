using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfaces
{
    internal class Ex2EmpClass2:Ex2TrainingInterface
    {
        public void Email()
        {
            Console.WriteLine("Email is emp2@gmail.com");
        }

        public void Name()
        {
            Console.WriteLine("Hi Name is Emp2");
        }
    }
}
