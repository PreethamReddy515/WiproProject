using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableThreadsIndexer
{
    internal class NullCoalescingOperatorEx1
    {
        static void Main()
        {
            string s1 = null;
            string s2 = "Welcome";
            string s3 = "Bye";

            string s4 = s1 ?? s2;
            //null - coalescing operator ?? returns the left-hand operand if it's not null, otherwise returns the right-hand operand.
            Console.WriteLine(s4);
            s4 = s3 ?? s2;
            Console.WriteLine(s4);
        }
    }
}
