using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class ArrayStr
    {
        public void Show()
        {
            string[] names = new string[]
            {
                "name1","name2","name3","name4","name5"
            };
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }

        }
        static void Main()
        {
            ArrayStr arrayStr = new ArrayStr();
            arrayStr.Show();
        }
    }
}
