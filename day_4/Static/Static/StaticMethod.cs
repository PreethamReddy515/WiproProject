using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    class Data
    {
        public void Show()
        {
            Console.WriteLine("Show Method from Class Data...");
        }

        public static void Display()
        {
            Console.WriteLine("Display Method from Class Data...");
        }
    }
    internal class StaticMethod
    {
        static void Main()
        { 
            Data.Display(); // calling method without creatig object for Data
            new Data().Show(); // created object of Data to call Show method
        }

    }
}
