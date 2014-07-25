namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Assertions
    {
        public static void Fail(string template, params object[] templateParams)
        {
            throw new AssertException(string.Format(template, templateParams));
        }
    }
}
