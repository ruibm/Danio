namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExecutionLog
    {
        private readonly string _logName;
        private readonly List<string> _logEntries;

        public ExecutionLog(string logName)
        {
            _logName = logName;
            _logEntries = new List<string>();
        }

        public static ExecutionLog CreateErrorLog()
        {
            return new ExecutionLog("error");
        }

        public static ExecutionLog CreateWarningLog()
        {
            return new ExecutionLog("warning");
        }

        public void Add(string template, params object[] templateParameters)
        {
            _logEntries.Add(string.Format(template, templateParameters));
        }

        public int Count
        {
            get { return _logEntries.Count; }
        }

        public string GetErrorsPrettyPrint()
        {
            StringBuilder builder = new StringBuilder();

            foreach (string error in _logEntries)
            {
                builder.AppendLine(error);
            }
            
            builder.AppendFormat("A total of [{0}] errors were found.", _logEntries.Count);
            builder.AppendLine();

            return builder.ToString();
        }

        public override string ToString()
        {
            return GetErrorsPrettyPrint();
        }
    }
}
