namespace Ruibm.Danio.ExampleWithNugetPackage
{
    using Ruibm.Danio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {

        [Arg("Just a string.")]
        public static string MyStringArg = "My lovely default value";


        [Arg("Just a string.")]
        private static string MyPrivateStringArg = "My other lovely default value";

        static void Main(string[] args)
        {
            Args.InitOrExitOnError(args);
            Console.WriteLine("[{0}]=[{1}]", "MyStringArg", MyStringArg);
            Console.WriteLine("[{0}]=[{1}]", "MyPrivateStringArg", MyPrivateStringArg);
            Console.WriteLine("Press Return to continue.");
            Console.ReadLine();
        }
    }
}
