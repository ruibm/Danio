namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class ParseResult
    {
        public bool Success { get; private set; }

        public MultiDictionary<string, string> Arguments { get; private set; }

        public ExecutionLog ErrorLog { get; private set; }

        public ParseResult(bool success, MultiDictionary<string, string> arguments, ExecutionLog errorLog) 
        {
            Success = success;
            Arguments = arguments;
            ErrorLog = errorLog;
        }
    }
}
