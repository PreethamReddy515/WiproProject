using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class Assignment1
    {
        public void Show(string data)
        {
            string[] names = data.Split(' ');
            int flag = 0;
            string res = "";
            foreach (string name in names)
            {
                if (flag == 0)
                {
                    flag = 1;
                    res = res+ name+" ";
                }
                else if (flag == 1)
                {
                    char[] charArray = name.ToCharArray();
                    Array.Reverse(charArray);
                    string rev = new string(charArray);
                    flag = 0;
                    res = res+ rev + " ";
                }
                
            }
            Console.WriteLine(res);
        }
        static void Main()
        {
            string data;
            Console.WriteLine("Enter a String  ");
            data = Console.ReadLine();
            Assignment1 obj = new Assignment1();
            obj.Show(data);
        }
    }
}
