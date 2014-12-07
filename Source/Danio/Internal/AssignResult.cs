namespace Ruibm.Danio.Internal
{
    public class AssignResult
    {
        public bool Success { get; private set; }

        public ExecutionLog ErrorLog { get; private set; }

        public AssignResult(ExecutionLog errorLog)
        {
            Success = (errorLog.Count == 0);
            ErrorLog = errorLog;
        }
    }
}
