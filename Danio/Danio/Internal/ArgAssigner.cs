namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class ArgAssigner
    {
        public AssignResult AssignValues(FindResult findResult, ParseResult parseResult)
        {
            ExecutionLog errorLog = ExecutionLog.CreateErrorLog();
            HashSet<string> mandatoryFullNames = new HashSet<string>(findResult.IndexedInstances.MandatoryFullNames);

            foreach (string argName in parseResult.Arguments.Keys)
            {
                List<ArgInstance> instances = findResult.IndexedInstances.GetArgInstancesForName(argName);
                var instancesForNae = findResult.IndexedInstances.GetArgInstancesForName(argName);
                if (instances.Count == 0)
                {
                    errorLog.Add("Arg named [{0}] does not exist.", argName);
                }
                else if (instances.Count > 1)
                {
                    errorLog.Add(
                        "Arg [{0}] is ambiguous. It can refer to: [{1}].",
                        ToStringByFullNames(instances));
                }
                else
                {
                    ArgInstance instance = instances[0];
                    SetArgInstanceValueOrAddError(instance, parseResult.Arguments[argName], errorLog);
                    mandatoryFullNames.Remove(instance.FullName);
                }
            }

            if (mandatoryFullNames.Count > 0)
            {
                List<string> sortedMandatoryArgsNotSet = new List<string>(mandatoryFullNames);
                sortedMandatoryArgsNotSet.Sort();
                errorLog.Add("Mandatory Args [{0}] were not explicitly set.",
                    string.Join(", ", sortedMandatoryArgsNotSet));
            }

            return new AssignResult(errorLog);
        }

        private void SetArgInstanceValueOrAddError(ArgInstance instance, List<string> values, ExecutionLog errorLog)
        {
            Type fieldType = instance.Field.FieldType;
            if (fieldType.IsArray)
            {
                instance.Field.SetValue(instance.ParentObject, CreateFieldValueForArray(fieldType, values));
            }
            else
            {
                if (values.Count == 1)
                {
                    instance.Field.SetValue(instance.ParentObject, CreateFieldValue(fieldType, values[0]));
                }
                else
                {
                    errorLog.Add("Multiple values passed for argument [{0}]. Values are [{1}].",
                        instance.FullName,
                        string.Join(", ", values));
                }
            }
        }

        private object CreateFieldValueForArray(Type fieldType, List<string> values)
        {
            List<string> allValuesExpanded = new List<string>();
            foreach (string currentValue in values)
            {
                allValuesExpanded.AddRange(currentValue.Split(',').ToList<string>());
            }

            Array myArray = (Array)Activator.CreateInstance(fieldType, allValuesExpanded.Count);
            for (int i = 0; i < allValuesExpanded.Count; ++i) 
            {
                myArray.SetValue(CreateFieldValue(fieldType.GetElementType(), allValuesExpanded[i]), i);
            }

            return myArray;
        }

        private static object CreateFieldValue(Type fieldType, string value)
        {
            if (fieldType.IsEnum)
            {
                long parsedInt = -1;
                if (long.TryParse(value, out parsedInt))
                {
                    return Enum.ToObject(fieldType, parsedInt);
                }
                else
                {
                    return Enum.Parse(fieldType, value);
                }
            }
            return Convert.ChangeType(value, fieldType);
        }
        
        public static string ToStringByFullNames(List<ArgInstance> instances)
        {
            if (instances.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(instances[0].FullName);
            for (int i = 1; i < instances.Count; ++i)
            {
                builder.Append(", " + instances[i].FullName);
            }
            return builder.ToString();
        }
    }
}
