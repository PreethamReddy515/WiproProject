using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static interfaces.multipleInherited;

namespace interfaces
{
    internal class Ex1Program
    {
        static void Main(string[] args)
        {
            Ex1multipleInherited demo = new Ex1multipleInherited();
            demo.Name();
            demo.Email();
        }
    }
}
