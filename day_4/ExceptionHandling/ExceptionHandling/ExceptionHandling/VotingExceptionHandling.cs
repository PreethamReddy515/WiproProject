using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    internal class VotingExceptionHandling
    {
        public void Check(int age)
        {
            if (age < 18)
            {
                throw new VotingCustomException("You are Not Eligible For voting...");
            }
            Console.WriteLine("You Can Vote...");
        }
        static void Main()
        {
            int age;
            Console.WriteLine("Enter Age  ");
            age = Convert.ToInt32(Console.ReadLine());
            VotingExceptionHandling voting = new VotingExceptionHandling();
            try
            {
                voting.Check(age);
            }
            catch (VotingCustomException v)
            {
                Console.WriteLine(v.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
