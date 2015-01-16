namespace Ruibm.Danio.Example1
{
    using Ruibm.Danio;
    using System;
    using System.Diagnostics;
    using System.Linq;

    public class Program
    {
        public enum MyEnumType
        {
            EnumValue1,
            EnumValue2,
            EnumValue3
        }

        [Arg("The start date.")]
        public static DateTime Start = DateTime.UtcNow.AddDays(-1);

        [Arg("The end date.")]
        public static DateTime End = DateTime.UtcNow;

        [Arg("A boolean value.")]
        public static bool MySuperBool = true;

        [Arg("A public int arg.", IsMandatory=true)]
        public static int MyPublicIntArg = 42;

        [Arg("An int with allowed values.", AllowedValues = new object[] { 10, 12, 21 })]
        private static int MyIntArg;

        [Arg("Just a string.")]
        private static string MyStringArg = "My lovely default value";

        [Arg("A lovely enum.")]
        private static MyEnumType MyEnumArg = MyEnumType.EnumValue3;

        [Arg("An int array.")]
        private static int[] MyIntArrayArg;

        [Arg("A string array.")]
        private static string[] MyStringArrayArg = new[]
        {
            "value1",
            "another good value",
            "113123"
        };

        static void Main(string[] args)
        {
            Args.InitOrExitOnError(args);

            Console.WriteLine("[{0}]=[{1}]", "MyPublicIntArg", MyPublicIntArg);
            Console.WriteLine("[{0}]=[{1}]", "MyIntArg", MyIntArg);
            Console.WriteLine("[{0}]=[{1}]", "MyStringArg", MyStringArg);
            Console.WriteLine("[{0}]=[{1}]", "MyEnumArg", MyEnumArg);
            Console.WriteLine("[{0}]=[{1}]", "MyIntArrayArg", MyIntArrayArg == null ?
                "null" : string.Join(", ", MyIntArrayArg));
            Console.WriteLine("[{0}]=[{1}]", "MyStringArrayArg", MyStringArrayArg == null ?
                "null" : string.Join(", ", MyStringArrayArg));
            Console.WriteLine("[{0}]=[{1}]", "MySuperBool", MySuperBool);
            Console.WriteLine("Press Return to continue.");
            Console.ReadLine();
        }
    }
}
