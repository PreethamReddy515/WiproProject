using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTraining
{
    internal class TrainingMain
    {
        static void Main(string[] args)
        {
            TrainingAbstractClass[] arrTraining = new TrainingAbstractClass[]
            {
                new Trainee1(),
                new Trainee2(),
                new Trainee3()
            };

            foreach (TrainingAbstractClass t in arrTraining)
            {
                t.Company();
                t.Email();
                t.Name();
                Console.WriteLine("-----------------------------");
            }
        }
    }
}
