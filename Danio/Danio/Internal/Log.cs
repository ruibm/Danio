// Enable/disable this line to help debug.
// #define ARGS_LOG

namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Log
    {
        public static void Info(string format, params object[] args)
        {
#if ARGS_LOG
            Trace.TraceInformation(format, args);
#endif
        }
        public static void Warning(string format, params object[] args)
        {
#if ARGS_LOG
            Trace.TraceWarning(format, args);
#endif

        }
        public static void Error(string format, params object[] args)
        {
#if ARGS_LOG
            Trace.TraceError(format, args);
#endif

        }
    }
}
