using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3ArraysReferences
{
    internal class ReferenceEx
    {
        public void Increment(ref int x)
        {
            ++x;
            //not returning x
        }
        static void Main()
        {
            int x = 12;
            ReferenceEx obj = new ReferenceEx();

            obj.Increment(ref x);
            Console.WriteLine(x);
            //value of x increased
        }
    }
}
