using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableThreadsIndexer
{
    internal class NullableEx2
    {
        static void Main()
        {
            Employ employ1 = new Employ();
            employ1.Empno = 1;
            employ1.Name = "Anusha";
            employ1.Basic = 88323;

            Employ employ2 = new Employ();
            employ2.Empno = 2;
            employ2.Name = "Raghav";
            employ2.Basic = 81233;
            employ2.LeaveDays = 3;

            Console.WriteLine("{0} status is {1} ",employ2.Name, employ2.Status);

            if (employ1.LeaveDays.HasValue)
            {
                Console.WriteLine("{0} taken leave ", employ1.Name);
                employ1.Status = 1;
            }
            else
            {
                Console.WriteLine("{0} Not Taken Leave  ", employ1.Name);
                employ1.Status = 0;
            }

            if (employ2.LeaveDays.HasValue)
            {
                Console.WriteLine("{0} taken leave ", employ2.Name);
                employ2.Status = 0;
            }
            else
            {
                Console.WriteLine("{0} Not Taken Leave  " + employ2.Name);
                employ2.Status = 1;
            }
            Console.WriteLine("{0} status is {1} ", employ2.Name, employ2.Status);

        }
    }
}
