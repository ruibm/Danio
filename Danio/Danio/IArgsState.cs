namespace Ruibm.Danio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IArgsState
    {
        bool IsValid { get; }

        string GetUsage();

        string GetErrors();
    }
}
