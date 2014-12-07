namespace Ruibm.Danio.Internal
{
    using System;

    public interface IParser
    {
        ParseResult ParseArgs(string[] args);
    }
}
