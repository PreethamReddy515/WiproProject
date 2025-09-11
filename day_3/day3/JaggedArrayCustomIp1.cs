using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3ArraysReferences
{
    internal class JaggedArrayCustomIp1
    {
        static void Main()
        {
            int n, m;
            Console.WriteLine("Enter Sizes of elements in each array (for 2 arrays)");
            n = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());

            int[][] jaggedArray = new int[n][];

            int[] x = new int[n];
            int[] y = new int[m];

            Console.WriteLine("Enter Elements {0} for Array X ", n);
            for (int i = 0; i < n; i++)
            {
                x[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Enter Elements {0} for Array Y ", m);
            for (int i = 0; i < m; i++)
            {
                y[i] = Convert.ToInt32(Console.ReadLine());
            }
            jaggedArray[0] = x;
            jaggedArray[1] = y;

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write(jaggedArray[i][j] + "    ");
                }
                Console.WriteLine();
            }
        }
    }
}
