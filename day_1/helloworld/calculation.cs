using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloworld
{
    internal class calculation
    {
        static void Main()
        {
            int a, b, c;
            Console.WriteLine("enter 2 numbers ");
            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            c = a + b;
            Console.WriteLine("sum "+c);
            c = a - b;
            Console.WriteLine("sub " + c);
            c = a * b;
            Console.WriteLine("prdct " + c);
        }
    }
}
