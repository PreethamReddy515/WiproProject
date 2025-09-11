using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsOnFiles
{
    internal class BinaryWriterEx
    {
        static void Main()
        {
            FileStream fs = new FileStream(@"C:\Users\nehaa\wiproData\day5\Files\Data.txt", FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(12);
            bw.Write("Wipro");
            bw.Write(28832.33);
            bw.Write(true);
            bw.Close();
            fs.Close();
            Console.WriteLine("Primitive Data Stored in files...");
        }
    }
}
