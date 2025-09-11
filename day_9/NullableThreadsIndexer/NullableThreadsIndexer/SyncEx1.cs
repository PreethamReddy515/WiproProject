using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NullableThreadsIndexer
{
    class Data
    {
        public void Show(string name)
        {
            lock (this)
            {
                Console.Write("Hello  " + name);
                Thread.Sleep(1000);
                Console.WriteLine(" How are You...");
            }
        }
    }
    internal class SyncEx1
    {
        Data data;
        SyncEx1(Data data)
        {
            this.data = data;
        }

        public void Rajesh()
        {
            data.Show("Rajesh");
        }

        public void Venkata()
        {
            data.Show("Venkata");
        }

        static void Main()
        {
            SyncEx1 syncEx1 = new SyncEx1(new Data());
            ThreadStart th1 = new ThreadStart(syncEx1.Rajesh);
            ThreadStart th2 = new ThreadStart(syncEx1.Venkata);
            Thread t1 = new Thread(th1);
            Thread t2 = new Thread(th2);

            t1.Start();
            t2.Start();
        }
    }
}
