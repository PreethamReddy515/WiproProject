using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTraining
{
    internal class Trainee1 : TrainingAbstractClass
    {
        public override void Email()
        {
            Console.WriteLine("Email is trainee1@gmail.com");
        }

        public override void Name()
        {
            Console.WriteLine("Hi my name is trainee1");
        }
    }
}
