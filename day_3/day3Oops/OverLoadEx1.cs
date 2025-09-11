using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3Oops
{
    internal class OverLoadEx1
    {
        
            public void Show(int x)
        {
            Console.WriteLine("Show Method w.r.t. int  " + x);
        }

        public void Show(double x)
        {
            Console.WriteLine("Show Method w.r.t. double  " + x);
        }

        public void Show(string x)
        {
            Console.WriteLine("Show Method w.r.t. String  " + x);
        }
        static void Main(string[] args)
        {
            OverLoadEx1 obj = new OverLoadEx1();
            obj.Show(12);
            obj.Show("Wipro");
            obj.Show(523.33);
        }
    }
}
