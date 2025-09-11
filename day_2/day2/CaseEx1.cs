using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class CaseEx1
    {
        public void Check(char choice)
        {
            switch (choice)
            {
                case 'a':
                case 'A':
                case '1':
                    Console.WriteLine("Hi I am A");
                    break;
                case 'b':
                case 'B':
                case '2':
                    Console.WriteLine("Hi I am B...");
                    break;
                case 'c':
                case 'C':
                case '3':
                    Console.WriteLine("Hi I am C...");
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
        static void Main()
        {
            char choice;
            Console.WriteLine("Enter Your Choice  ");
            choice = Convert.ToChar(Console.ReadLine());
            CaseEx1 obj = new CaseEx1();
            obj.Check(choice);
        }
    }
}
