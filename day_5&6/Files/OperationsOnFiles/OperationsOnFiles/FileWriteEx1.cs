using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsOnFiles
{
    internal class FileWriteEx1
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"C:\Users\nehaa\wiproData\day5\Files\Demo1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("This program belongs to Wipro Cohert4...");
            sw.WriteLine("Thank you All...");
            sw.WriteLine("Trainer is Prasanna...");
            sw.Flush();
            sw.Close();
            fs.Close();
            Console.WriteLine("File Created Successfully...");
        }
    }
}
