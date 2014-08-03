namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ConsoleArgsState : IArgsState
    {
        private readonly FindResult _findResult;
        private readonly ParseResult _parseResult;
        private readonly AssignResult _assignResult;

        public ConsoleArgsState(string[] args)
        {
            ArgFinder finder = new ArgFinder();
            _findResult = finder.FindAllArgs(Assembly.GetEntryAssembly());

            ConsoleParser parser = new ConsoleParser();
            _parseResult = parser.ParseArgs(args);

            ArgAssigner assigner = new ArgAssigner();
            _assignResult = assigner.AssignValues(_findResult, _parseResult);            
        }
        
        public string GetUsage()
        {
            HelpBuilder help = new HelpBuilder(_findResult);
            return help.GetUsage();
        }

        public string GetErrors()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("=== Errors ===");
            if (_findResult != null && !_findResult.Success)
            {
                builder.Append(_findResult.ErrorLog.GetErrorsPrettyPrint());
            }
            else if (_parseResult != null && !_parseResult.Success)
            {
                builder.Append(_parseResult.ErrorLog.GetErrorsPrettyPrint());
            }
            else if (_assignResult != null && !_assignResult.Success)
            {
                builder.Append(_assignResult.ErrorLog.GetErrorsPrettyPrint());
            }
            else
            {
                return string.Empty;
            }

            return builder.ToString();
        }

        public bool IsValid
        {
            get 
            {
                return _findResult.Success && _parseResult.Success && _assignResult.Success;
            }
        }
    }
}
