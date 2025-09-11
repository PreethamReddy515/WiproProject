using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3ArraysReferences
{
    internal class FactorialRef
    {
        public void Calc(int n, ref int f)
        {

            for (int i = 1; i <= n; i++)
            {
                f = f * i;
            }
            // we are not returning f, but value of f is reflected, because of refernce passing
        }
        static void Main()
        {
            int n, f = 1;
            Console.WriteLine("Enter N value   ");
            n = Convert.ToInt32(Console.ReadLine());
            FactorialRef obj = new FactorialRef();
            obj.Calc(n, ref f);
            Console.WriteLine("Factorial Value  " + f);
        }
    }
}
