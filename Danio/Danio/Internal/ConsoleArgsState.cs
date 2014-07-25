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

        public bool IsValid
        {
            get 
            {
                return _findResult.Success && _parseResult.Success && _assignResult.Success;
            }
        }
    }
}
