using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesExample
{
    class Bank
    {
        public int AccountNo { get; } = 12;
        public string BranchName { get; } = "ECIL";
        public string BankName { get; } = "ICICI";
    }
    internal class ReadOnlyEx1
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            //bank.AccountNo = 12; it gives error becuase read only
            Console.WriteLine("Account No  " + bank.AccountNo);
            Console.WriteLine("Bank Name  " + bank.BankName);
            Console.WriteLine("Branch Name  " + bank.BranchName);
        }
    }
}
