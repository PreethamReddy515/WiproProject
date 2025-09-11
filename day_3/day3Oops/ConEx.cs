using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3Oops
{
    internal class ConEx
    {
        static ConEx()
        {
            Console.WriteLine("Static Constructor...");
        }

        ConEx()
        {
            Console.WriteLine("Instance Constructor...");
        }
        static void Main()
        {
            ConEx obj = new ConEx();
        }
    }
}
