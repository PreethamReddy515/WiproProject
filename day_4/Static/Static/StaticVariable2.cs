using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    internal class StaticVariable2
    {
        static int score;
        public void Increment(int x)
        {
            score += x;
        }

        static void Main()
        {
            StaticVariable2 fb = new StaticVariable2();
            StaticVariable2 sb = new StaticVariable2();
            StaticVariable2 ext = new StaticVariable2();

            fb.Increment(14);
            sb.Increment(9);
            ext.Increment(11);

            Console.WriteLine("Total Score  " + StaticVariable2.score);
        }
    }
}
