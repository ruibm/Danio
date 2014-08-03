namespace Ruibm.Danio
{
    using Ruibm.Danio.Internal;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    public static class Args
    {
        public static IArgsState Init(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            ConsoleArgsState state = new ConsoleArgsState(args);
            
            Log.Info("Args.Init took [{0}] milliseconds to run.", 
                stopwatch.ElapsedMilliseconds);

            return state;
        }

        public static void InitOrExitOnError(string[] args)
        {
            IArgsState state = Init(args);
            if (!state.IsValid)
            {
                Console.Error.WriteLine(state.GetUsage());
                Console.Error.WriteLine(state.GetErrors());
                Environment.Exit(-1);
            }
        }
    }
}
