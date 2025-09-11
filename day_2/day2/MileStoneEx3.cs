using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class MileStoneEx3
    {
        public bool IsPalindrome(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            string rev = new string(chars);
            //string rev = "";
            //for (int i = chars.Length - 1; i >= 0; i--)
            //{
            //    rev += chars[i];
            //}
            //Console.WriteLine(rev);
            if (rev.Equals(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Show(string data)
        {
            string[] words = data.Split(' ');
            foreach (string s in words)
            {
                if (IsPalindrome(s) == true)
                {
                    Console.WriteLine(s);
                }
            }
        }
        static void Main()
        {
            string data;
            Console.WriteLine("Enter a Sentence with palindrome words  ");
            data = Console.ReadLine();
            MileStoneEx3 obj = new MileStoneEx3();
            obj.Show(data);
        }
    }
}
