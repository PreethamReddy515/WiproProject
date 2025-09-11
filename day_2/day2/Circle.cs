using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class Circle
    {
        public void Calc(double radius)
        {
            double area, circ;
            area = Math.PI * radius * radius;
            circ = 2 * Math.PI * radius;
            Console.WriteLine("Area of Circle  " + area);
            Console.WriteLine("Circumference  " + circ);
        }
        static void Main()
        {
            double radius;
            Console.WriteLine("Enter Radius  ");
            radius = Convert.ToDouble(Console.ReadLine());
            Circle circle = new Circle();
            circle.Calc(radius);
        }
    }
}
