namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ErrorLog
    {
        private readonly List<string> _errors;

        public ErrorLog()
        {
            _errors = new List<string>();
        }

        public void Add(string template, params string[] templateParameters)
        {
            _errors.Add(string.Format(template, templateParameters));
        }

        public int Count
        {
            get { return _errors.Count; }
        }

        public string PrettyPrintErrors()
        {
            StringBuilder builder = new StringBuilder();

            foreach (string error in _errors)
            {
                builder.AppendLine(error);
            }
            
            builder.AppendFormat("A total of [{0}] errors were found.", _errors.Count);
            builder.AppendLine();

            return builder.ToString();
        }

        public override string ToString()
        {
            return PrettyPrintErrors();
        }
    }
}
