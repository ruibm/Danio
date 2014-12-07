namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FindResult
    {
        private List<ArgInstance> args;
        private ExecutionLog warningLog;
        private ExecutionLog errorLog;

        public List<ArgInstance> Instances { get; private set; }

        public IndexedArgInstances IndexedInstances { get; private set; }

        public ExecutionLog WarningLog { get; private set; }

        public ExecutionLog ErrorLog { get; private set; }

        public bool Success { get; private set; }

        public FindResult(List<ArgInstance> instances, ExecutionLog errorLog, ExecutionLog warningLog)
        {
            Instances = instances;
            IndexedInstances = new IndexedArgInstances(instances);
            ErrorLog = errorLog;
            WarningLog = warningLog;
            Success = (errorLog.Count == 0);
        }
    }
}
