using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloworld
{
    internal class Quiz5
    {
        int x;
        static void Main()
        {
            Quiz5 obj1 = new Quiz5();
            obj1.x = 12;
            Quiz5 obj2 = obj1;
            obj2.x = 13;
            Console.WriteLine(obj1.x);

            Console.WriteLine(obj1.GetHashCode());
            Console.WriteLine(obj2.GetHashCode());
        }
    }
}
