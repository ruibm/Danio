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
        private ErrorLog errorLog;

        public List<ArgInstance> Instances { get; private set; }

        public IndexedArgInstances IndexedInstances { get; private set; }

        public ErrorLog ErrorLog { get; private set; }

        public bool Success { get; private set; }

        public FindResult(List<ArgInstance> instances, ErrorLog errorLog)
        {
            Instances = instances;
            IndexedInstances = new IndexedArgInstances(instances);
            ErrorLog = errorLog;
            Success = (errorLog.Count == 0);
        }
    }
}
