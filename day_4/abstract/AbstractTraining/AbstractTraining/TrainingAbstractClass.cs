using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTraining
{
    internal abstract class TrainingAbstractClass
    {
        public void Company()
        {
            Console.WriteLine("Company is Wipro...");
        }
        //in abstrct class, its not neccessary that all methods are abstract
        public abstract void Name();
        public abstract void Email();
    }
}
