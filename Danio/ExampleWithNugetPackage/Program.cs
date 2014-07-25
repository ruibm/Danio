namespace Ruibm.Danio.ExampleWithNugetPackage
{
    using Ruibm.Args;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {

        [Arg("Just a string.")]
        private static string MyStringArg = "My lovely default value";

        static void Main(string[] args)
        {
            Args.InitOrExitOnError(args);
            Console.WriteLine("[{0}]=[{1}]", "MyStringArg", MyStringArg);
            Console.WriteLine("Press Return to continue.");
            Console.ReadLine();
        }
    }
}
