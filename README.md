# Danio: Args Parser for .NET


## Overview

Danio is a .NET library to parse command line arguments implemented with the same design as [gflags](https://code.google.com/p/gflags/) for C++.

You simply need to define your command line arguments (flags) as static fields in the classes where they are actually used in. Add the Attribute **[Arg("My flag help.")]** to the field in question and it is automatically parsed and assigned when the application starts.


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
