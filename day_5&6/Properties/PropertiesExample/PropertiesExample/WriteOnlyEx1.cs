using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesExample
{
    class Vendor
    {
        int vendorId;
        string vendorName;

        public int VendorId
        {
            set { vendorId = value; }
        }

        public string VendorName
        {
            set { vendorName = value; }
        }
        public override string ToString()
        {
            return "Vendor Id  " + vendorId + " Vendor Name  " + vendorName;
        }
    }
    internal class WriteOnlyEx1
    {
        static void Main()
        {
            Vendor vendor = new Vendor();
            vendor.VendorId = 1;
            vendor.VendorName = "Zomoto";

            //Console.WriteLine(vendor.VendorId);
            //gives error because its set to write only
            
            Console.WriteLine(vendor);
        }
    }
}
