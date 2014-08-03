
namespace Ruibm.Danio.Internal
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
using System;
    using System.Reflection;

    public class HelpBuilder
    {
        private FindResult _findResult;

        public HelpBuilder(FindResult findResult)
        {
            _findResult = findResult;
        }

        public string GetUsage()
        {
            HashSet<string> printedInstances = new HashSet<string>(_findResult.Instances.Select(x => x.FullName));
            List<string> allNames = _findResult.IndexedInstances.GetAllArgumentNames();
            
            StringBuilder argumentsBuilder = new StringBuilder();
            argumentsBuilder.AppendLine("=== Args ===");

            StringBuilder usageBuilder = new StringBuilder();
            usageBuilder.AppendLine("=== Usage ===");
            usageBuilder.AppendFormat("  {0}", System.AppDomain.CurrentDomain.FriendlyName);
            foreach (string name in allNames)
            {
                List<ArgInstance> instances = _findResult.IndexedInstances.GetArgInstancesForName(name);                
                if (instances.Count == 1)
                {
                    // Do usage.
                    if (instances[0].Arg.IsMandatory)
                    {
                        usageBuilder.AppendFormat(" --{0} VALUE", instances[0].Name);
                    }

                    // Do arguments.
                    if (printedInstances.Contains(instances[0].FullName))
                    {
                        AppendArgInstanceUsage(argumentsBuilder, instances[0]);
                        printedInstances.Remove(instances[0].FullName);
                    }
                }
            }
            
            usageBuilder.AppendLine();
            usageBuilder.AppendLine();
            usageBuilder.Append(argumentsBuilder);
            return usageBuilder.ToString();
        }

        private static void AppendArgInstanceUsage(StringBuilder builder, ArgInstance instance)
        {
            List<string> names = new List<string>();
            if (instance.ShortName != null)
            {
                builder.AppendFormat("  -" + instance.ShortName + ", ");
            }
            else
            {
                builder.AppendFormat("      ");
            }

            if (instance.Name != null)
            {
                names.Add("--" + instance.Name);
            }

            // No need to print the FullName as all variables must have a Name.
            // It would also make the usage string quite verbose.
            builder.AppendFormat("{0, -20} {1}",
                string.Join(", ", names),
                instance.Arg.Help);

            if (instance.Arg.AllowedValues != null)
            {
                builder.AppendFormat(" (AllowedValues=[{0}])", string.Join(", ", instance.Arg.AllowedValues));
            }
            else if (instance.Field.FieldType.Equals(typeof(bool)))
            {
                builder.Append(" (AllowedValues=[True, False])");
            }

            if (instance.DefaultValue != null)
            {
                if (instance.DefaultValue.GetType().IsArray)
                {
                    builder.AppendFormat(" (DefaultValue=[{0}])", string.Join(", ", (Array)instance.DefaultValue));
                }
                else
                {
                    builder.AppendFormat(" (DefaultValue=[{0}])", instance.DefaultValue);
                }
            }

            builder.AppendLine();
        }
    } 
}
