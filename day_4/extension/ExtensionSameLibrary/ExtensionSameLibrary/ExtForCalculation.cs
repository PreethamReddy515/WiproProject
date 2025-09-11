using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionSameLibrary
{
    internal static class ExtForCalculation
    {
        public static int Mult(this Calculation calc, int a, int b)
        {
            return a * b;
        }
    }
    //extension has to be done in static class, static method only
}
