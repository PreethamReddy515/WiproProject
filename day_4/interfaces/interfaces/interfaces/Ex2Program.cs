using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfaces
{
    internal class Ex2Program
    {
        static void Main(string[] args)
        {
            Ex2TrainingInterface[] arr = new Ex2TrainingInterface[]
            {
                new Ex2EmpClass1(), new Ex2EmpClass2()
            };

            foreach (Ex2TrainingInterface i in arr)
            {
                i.Name();
                i.Email();
                Console.WriteLine("---------------------------");
            }
        }
    }
}
