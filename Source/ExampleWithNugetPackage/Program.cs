namespace Ruibm.Danio.ExampleWithNugetPackage
{
    using System;
    using Ruibm.Danio;

    class Program
    {

        [Arg("Just a string.")]
        public static string MyStringArg = "My lovely default value";


        [Arg("Just a string.")]
        private static string MyPrivateStringArg = "My other lovely default value";

        [Arg("A comma separated list of numbers.")]
        private static int[] MyCsvIntArg = new[] 
        {
            10, 
            100,
            1000,
            10000
        };

        static void Main(string[] args)
        {
            Args.InitOrExitOnError(args);
            Console.WriteLine("[{0}]=[{1}]", "MyStringArg", MyStringArg);
            Console.WriteLine("[{0}]=[{1}]", "MyPrivateStringArg", MyPrivateStringArg);
            Console.WriteLine("[{0}]=[{1}]", "MyCsvIntArg", string.Join(", ", MyCsvIntArg));
            Console.WriteLine("Press Return to continue.");
            Console.ReadLine();
        }
    }
}
