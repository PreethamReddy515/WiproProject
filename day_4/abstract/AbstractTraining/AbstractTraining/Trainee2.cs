using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTraining
{
    internal class Trainee2:TrainingAbstractClass
    {
        public override void Email()
        {
            Console.WriteLine("Email is trainee2@gmail.com");
        }

        public override void Name()
        {
            Console.WriteLine("Hi my name is trainee2");
        }
    }
}
