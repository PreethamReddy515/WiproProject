using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class MileStoneEx2
    {
        static void Main()
        {
            string str = "Welcome to dotnet programming wipro";
            string[] names = str.Split(' ');
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
