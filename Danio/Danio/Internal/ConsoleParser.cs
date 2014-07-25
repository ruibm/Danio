namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class ConsoleParser : IParser
    {
        private static readonly Regex argRegex = new Regex(
            @"^--?([a-zA-Z0-9_\-.]+)(?:=(.+))?$", 
            RegexOptions.Compiled);

        public ParseResult ParseArgs(string[] args)
        {
            MultiDictionary<string, string> parsedArguments = new MultiDictionary<string, string>();
            ErrorLog errorLog = new ErrorLog();

            for (int i = 0; i < args.Length; ++i)
            {
                string currentArg = args[i];
                Match match = argRegex.Match(currentArg);
                if (match.Success)
                {
                    if (match.Groups.Count == 3)
                    {
                        if (string.IsNullOrEmpty(match.Groups[2].Value)) 
                        {
                            // The value must come after the space in the next args position.
                            if (i + 1 >= args.Length)
                            {
                                errorLog.Add("Argument [{0}] does not set to a value.", currentArg);
                            }
                            else
                            {
                                parsedArguments.Add(match.Groups[1].Value, args[++i]);
                            }
                        } 
                        else 
                        {                            
                            // The current Arg is contains both key and value separated by '='.
                            parsedArguments.Add(match.Groups[1].Value, match.Groups[2].Value);
                        }
                    }
                    else
                    {
                        Assertions.Fail(
                            "Invalid parsed argument [{0}]. The group count was [{1}].", 
                            currentArg, 
                            match.Groups.Count);
                    }
                }
                else
                {
                    errorLog.Add("Unparseable argument [{0}].", currentArg);
                }                
            }

            return new ParseResult(errorLog.Count == 0, parsedArguments, errorLog);
        }
    }
}
