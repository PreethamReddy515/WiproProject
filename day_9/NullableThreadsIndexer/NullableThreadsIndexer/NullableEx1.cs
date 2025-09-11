using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableThreadsIndexer
{
    internal class NullableEx1
    {
        static void Main(string[] args)
        {
            int? count = null;
            //Console.WriteLine("count: "+count);
            Console.WriteLine("Def Value "+count.GetValueOrDefault());
            
            count = 5;
            Console.WriteLine("Upd Value "+count.GetValueOrDefault());
        }
    }
}
