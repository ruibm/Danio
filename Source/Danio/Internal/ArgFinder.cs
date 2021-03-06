﻿namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public class ArgFinder
    {
        private const BindingFlags FieldBindingFlags =
            (BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static) 
            &
            ~BindingFlags.Instance;

        public FindResult FindAllArgs(Assembly rootAssembly)
        {
            Stack<Assembly> assembliesToScan = new Stack<Assembly>();
            HashSet<string> scannedAssemblies = new HashSet<string>();
            List<ArgInstance> args = new List<ArgInstance>();
            ExecutionLog errorLog = ExecutionLog.CreateErrorLog();
            ExecutionLog warningLog = ExecutionLog.CreateWarningLog();

            assembliesToScan.Push(rootAssembly);
            while (assembliesToScan.Count > 0)
            {
                Assembly currentAssembly = assembliesToScan.Pop();
                scannedAssemblies.Add(currentAssembly.FullName);
                Log.Info("Scanning Assembly [{0}].", currentAssembly.FullName);

                foreach (AssemblyName childName in currentAssembly.GetReferencedAssemblies())
                {
                    try
                    {
                        Assembly child = Assembly.Load(childName);
                        if (!scannedAssemblies.Contains(child.FullName))
                        {
                            assembliesToScan.Push(child);
                        }
                    }
                    catch (FileLoadException e)
                    {
                        warningLog.Add("Unable to load DLL [{0}] with exception: [{1}]", childName, e);
                    }
                    catch (FileNotFoundException e)
                    {
                        warningLog.Add("Could not find DLL [{0}] with exception: [{1}]", childName, e);
                    }
                    catch (BadImageFormatException e)
                    {
                        warningLog.Add("Unable to load DLL [{0}] with exception: [{1}]", childName, e);
                    }
                }

                Type[] currentAssemblyTypes = null;
                try
                {
                    currentAssemblyTypes = currentAssembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    warningLog.Add("Unable to retrieve types for DLL [{0}] with exception: [{1}]", currentAssembly.FullName, e);
                    continue;
                }
                Assertions.IsNotNull(currentAssemblyTypes, "currentAssemblyTypes cannot be null.");                

                foreach (Type type in currentAssemblyTypes)
                {
                    foreach (FieldInfo field in type.GetFields(FieldBindingFlags))
                    {
                        ArgAttribute arg = field.GetCustomAttribute<ArgAttribute>();
                        if (arg != null)
                        {
                            object defaultValue = field.GetValue(type);
                            ArgInstance argInstance = new ArgInstance(arg, field, type, type, defaultValue);
                            args.Add(argInstance);
                            if (argInstance.Arg.AllowedValues != null && 
                                argInstance.Arg.AllowedValues.GetType().Equals(field.FieldType)) 
                            {
                                errorLog.Add(
                                    "The Type [{0}] of AllowedValues for Arg [{1}] does not match its original Type [{2}].",
                                    argInstance.Arg.AllowedValues.GetType().Name, 
                                    argInstance.FullName, 
                                    field.FieldType.Name);
                            }
                        }
                    }
                }
            }

            args = ClearDuplicates(args);
            return new FindResult(args, errorLog, warningLog);
        }

        private List<ArgInstance> ClearDuplicates(List<ArgInstance> args)
        {
            Dictionary<string, ArgInstance> withoutDuplicatesDict = new Dictionary<string, ArgInstance>();
            args.ForEach(x => withoutDuplicatesDict[x.FullName] = x);
            List<ArgInstance> withoutDuplicatesList = new List<ArgInstance>(withoutDuplicatesDict.Values);
            return withoutDuplicatesList;
        }
    }
}
