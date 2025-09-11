using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    internal class StaticVariable
    {
        static int count;// shared among all objects of this class
        public void Increment()
        {
            count++;
        }

        public void Show()
        {
            Console.WriteLine("Count is  " + count);
        }
        static void Main(string[] args)
        {
            StaticVariable obj1 = new StaticVariable();
            StaticVariable obj2 = new StaticVariable();
            StaticVariable obj3 = new StaticVariable();

            obj1.Increment();
            obj2.Increment();
            obj3.Increment();

            obj1.Show();
        }
    }
}
