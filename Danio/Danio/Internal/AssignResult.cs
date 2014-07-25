namespace Ruibm.Danio.Internal
{
    public class AssignResult
    {
        public bool Success { get; private set; }

        public ErrorLog ErrorLog { get; private set; }

        public AssignResult(ErrorLog errorLog)
        {
            Success = (errorLog.Count == 0);
            ErrorLog = errorLog;
        }
    }
}
