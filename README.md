# Danio: Args Parser for .NET


## Overview

Danio is a .NET library to manage command line arguments across c# files, libraries and binaries. This is done in a very clean way by defining your command line arguments are static fields in the classes where they are actually used. You just need to add the Attribute [Arg("My help message for this argument.")] to the field in question.


## Quick Example

```c#
namespace Ruibm.Danio.ExampleWithNugetPackage
{
    using System;
    using Ruibm.Danio;

    class Program
    {

        [Arg("Just a string.", IsMandatory=true)]
        public static string MyStringArg = "My lovely default value";

        [Arg("An int.", AllowedValues = new object[] { 10, 12, 21 })]
        private static int MyPrivateIntArg = 10;

        static void Main(string[] args)
        {
            Args.InitOrExitOnError(args);
            Console.WriteLine("[{0}]=[{1}]", "MyStringArg", MyStringArg);
            Console.WriteLine("[{0}]=[{1}]", "MyPrivateIntArg", MyPrivateIntArg);
            Console.WriteLine("Press Return to continue.");
            Console.ReadLine();
        }
    }
}
```  


## More Examples

For an example on how to use Danio please take a look at the Example1 project contained in this codebase.
https://github.com/ruibm/Danio/blob/master/Danio/Example1/Program.cs
https://github.com/ruibm/Danio/blob/master/Danio/ExampleWithNugetPackage/Program.cs


## Origin of the Name

Danio is the genus of a variety of fish such as the Zebra Fish. http://en.wikipedia.org/wiki/Danio

                  __,
               .-'_-'`
             .' {`
         .-'````'-.    .-'``'.
       .'(0)       '._/ _.-.  `\
      }     '. ))    _<`    )`  |
       `-.,\'.\_,.-\` \`---; .' /
            )  )       '-.  '--:
           ( ' (          ) '.  \
            '.  )      .'(   /   )
              )/      (   '.    /
                       '._( ) .'
                    jgs    ( (
                            `-.


## Links

Source Code: https://github.com/ruibm/Danio
Nuget Package: https://www.nuget.org/packages/Ruibm.Danio
