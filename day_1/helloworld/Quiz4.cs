using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloworld
{
    internal class Quiz4
    {
        static void Main()
        {
            int x = 12;
            Console.WriteLine(x++ + ++x + ++x + x++);
            // 12 + 14 + 15 + 15 

            x = 12;
            Console.WriteLine(x++ + x-- + --x + ++x + x++ + --x);
            // 12 + 13 + 11 + 12 + 12 + 12
        }
    }
}
