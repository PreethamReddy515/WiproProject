using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    internal class VotingCustomException:ApplicationException
    {
        public VotingCustomException(string error):base(error) { }
    }
}
