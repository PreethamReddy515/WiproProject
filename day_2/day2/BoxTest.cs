using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class BoxTest
    {
        public void Show(object ob)
        {
            string type = ob.GetType().Name;
            Console.WriteLine(type);
            if (type.Equals("Int32"))
            {
                int x = (Int32)ob;
                Console.WriteLine("Integer Casting  " + x);
            }
            else if (type.Equals("String"))
            {
                string x = (string)ob;
                Console.WriteLine("String Casting  " + x);
            }
            else if (type.Equals("Double"))
            {
                double x = (Double)ob;
                Console.WriteLine("Double Casting  " + x);
            }
        }
        static void Main()
        {
            int x = 12;
            string str = "Wipro";
            double y = 12.55;

            BoxTest obj = new BoxTest();
            obj.Show(x);
            obj.Show(y);
            obj.Show(str);
        }

    }
}
