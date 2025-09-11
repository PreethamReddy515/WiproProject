using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3ArraysReferences
{
    internal class Array2D
    {
        static void Main()
        {
            int[,] x = new int[2, 3]
            {
                {1,2,3},
                {4,5,6}
            };

            Console.WriteLine("Elements of array");
            Console.WriteLine(x[0, 0]);
            Console.WriteLine(x[0, 1]);
            Console.WriteLine(x[0, 2]);
            Console.WriteLine("--------------------------------------");

            Console.WriteLine(x[1, 0]);
            Console.WriteLine(x[1, 1]);
            Console.WriteLine(x[1, 2]);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Print using for loop");

            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.WriteLine("x{0}{1}  {2}", i, j, x[i, j]);
                }

            }
            Console.WriteLine();

            Console.WriteLine("Matrix format");
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(x[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
