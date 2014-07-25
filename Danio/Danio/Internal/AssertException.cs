using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruibm.Danio.Internal
{
    public class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}
