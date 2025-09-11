using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableThreadsIndexer
{
    public class Employ
    {
        public int Empno { get; set; }
        public string Name { get; set; }
        public double Basic { get; set; }
        public int? LeaveDays { get; set; } // Nullable using shorthand syntax

        public Nullable<int> Status { get; set; } // Nullable using generic type syntax
    }
}
