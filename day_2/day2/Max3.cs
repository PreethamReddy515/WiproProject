using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class Max3
    {
        public void Show(int a, int b, int c)
        {
            int m = a;
            if (m < b)
            {
                m = b;
            }
            if (m < c)
            {
                m = c;
            }
            Console.WriteLine("Max Value  " + m);
        }
        static void Main()
        {
            int a, b, c;
            Console.WriteLine("Enter 3 Numbers  ");
            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            c = Convert.ToInt32(Console.ReadLine());
            Max3 max3 = new Max3();
            max3.Show(a, b, c);
        }
    }
}
