using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day2
{
    internal class CelToFar
    {
        public double ConvertToFahrenehit(double celcius)
        {
            double f = ((9 * celcius) / 5) + 32;
            return f;
        }
        static void Main()
        {
            double celsius;
            Console.WriteLine("Enter Celsius Value  ");
            celsius = Convert.ToDouble(Console.ReadLine());
            CelToFar obj = new CelToFar();
            Console.WriteLine("Fahrenheit Value is  " + obj.ConvertToFahrenehit(celsius));
        }
    }
}
