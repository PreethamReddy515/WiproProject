using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3ArraysReferences
{
    internal class References
    {
        int n;
        static void Main()
        {
            int x = 12;
            References obj1= new References();
            obj1.n = 12;
            References obj2= new References();
            obj2.n = 13;
            Console.WriteLine(obj1.n);//12

            Console.WriteLine(obj1.GetHashCode());
            Console.WriteLine(obj2.GetHashCode());//different value
        }
    }
}
