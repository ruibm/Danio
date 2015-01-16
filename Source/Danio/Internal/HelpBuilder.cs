namespace Ruibm.Danio.Internal
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System;
    using System.Reflection;

    public class HelpBuilder
    {
        [Arg("Show the usage message for this program.")]
        public static bool help = false;

        private const int HelpStringStartIndex = 20;
        private const int MinCharactersForHelp = 10;

        private FindResult _findResult;

        public HelpBuilder(FindResult findResult)
        {
            _findResult = findResult;
        }

        public string GetUsage(int maxCharactersPerLine)
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
                    // Do arguments.
                    if (printedInstances.Contains(instances[0].FullName))
                    {
                        // Do usage.
                        if (instances[0].Arg.IsMandatory)
                        {
                            usageBuilder.AppendFormat(" --{0}=VALUE", instances[0].Name);
                        }

                        argumentsBuilder.Append(GetArgInstanceUsage(maxCharactersPerLine, instances[0]));                        
                        printedInstances.Remove(instances[0].FullName);
                    }
                }
            }
            
            usageBuilder.AppendLine();
            usageBuilder.AppendLine();
            usageBuilder.Append(argumentsBuilder);
            return usageBuilder.ToString();
        }

        private static string GetArgInstanceUsage(int maxCharactersPerLine, ArgInstance instance)
        {
            StringBuilder builder = new StringBuilder();
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
            builder.AppendFormat(
                "{0, -" + HelpStringStartIndex.ToString() + "} {1}",
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
            else if (instance.Field.FieldType.IsEnum)
            {
                Array enumValues = Enum.GetValues(instance.Field.FieldType);
                List<string> stringValues = new List<string>(); 
                for (int i = 0; i < enumValues.Length; ++i)
                {
                    stringValues.Add(enumValues.GetValue(i).ToString());
                }
                builder.AppendFormat(" (AllowedValues=[{0}])", string.Join(", ", stringValues));
            }

            if (instance.DefaultValue != null)
            {
                if (instance.DefaultValue.GetType().IsArray)
                {
                    builder.AppendFormat(" (DefaultValue=[{0}])", string.Join(", ", ((Array)instance.DefaultValue).Cast<object>()));
                }
                else
                {
                    builder.AppendFormat(" (DefaultValue=[{0}])", instance.DefaultValue);
                }
            }
            return GetInMultipleLines(builder, maxCharactersPerLine); ;
        }

        private static string GetInMultipleLines(StringBuilder builder, int maxCharactersPerLine)
        {
            // Off by one correction otherwise the Console will auto breakline.
            maxCharactersPerLine -= 1;

            string initialString = builder.ToString();
            int charactersForHelp = Math.Max(maxCharactersPerLine - HelpStringStartIndex, MinCharactersForHelp);
            builder.Clear();
            int lengthToAdd = Math.Min(maxCharactersPerLine, initialString.Length);
            builder.Append(initialString.Substring(0, lengthToAdd));
            builder.AppendLine();
            for (int i = lengthToAdd; i < initialString.Length;)
            {
                lengthToAdd = Math.Min(initialString.Length - i, charactersForHelp);
                builder.AppendFormat(
                    "{0, -26} {1}",
                    string.Empty,
                    initialString.Substring(i, lengthToAdd));
                builder.AppendLine();
                i += lengthToAdd;
            }
            return builder.ToString();
        }
    } 
}
