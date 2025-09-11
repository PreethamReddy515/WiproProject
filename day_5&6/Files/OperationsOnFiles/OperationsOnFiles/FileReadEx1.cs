using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsOnFiles
{
    internal class FileReadEx1
    {
        static void Main()
        {
            FileStream fs=new FileStream(@"C:\Users\nehaa\wiproData\day5\Files\Demo1.txt",FileMode.Open,FileAccess.Read);
            StreamReader sr=new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            sr.Close();
            fs.Close();
        }
    }
}
