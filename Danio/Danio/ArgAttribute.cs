namespace Ruibm.Danio
{
    using System;

    /// <summary>
    /// C# Attribute that marks any static field as an argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ArgAttribute : Attribute
    {
        /// <summary>
        /// The short name for this argument.
        /// </summary>
        public char? ShortName { get; set; }

        /// <summary>
        /// Whether this is an Optional or Mandatory argument.
        /// </summary>
        public ArgType Type { get; set; }

        /// <summary>
        /// Array of all possible values this argument can take.
        /// If null then this is ignored.
        /// </summary>
        public object[] AllowedValues { get; set; }

        /// <summary>
        /// The help message for this argument.
        /// </summary>
        public string Help { get; set; }

        public ArgAttribute(string help)
        {
            Help = help;
        }
    }
}
