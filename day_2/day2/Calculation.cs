using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class Calculation
    {
        #region methods
        public int Sum(int a, int b)
        {
            return (a + b);
        }
        public int Sub(int a, int b)
        {
            return a - b;
        }
        public int Mult(int a, int b)
        {
            return a * b;
        }
        #endregion methods
        #region main_method
        static void Main()
        {
            Console.WriteLine("Enter first number: ");
            int a=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter second number: ");
            int b= Convert.ToInt32(Console.ReadLine());

            Calculation c = new Calculation();
            int s = c.Sum(a,b);
            Console.WriteLine("sum: "+s);
            s = c.Sub(a, b);
            Console.WriteLine("Sub is  " + s);
            s = c.Mult(a, b);
            Console.WriteLine("Mult is  " + s);
        }
        #endregion main_method
    }
}
