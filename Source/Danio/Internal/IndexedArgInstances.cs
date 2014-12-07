namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IndexedArgInstances
    {
        private readonly MultiDictionary<string, ArgInstance> _argInstancesByName;
        private readonly HashSet<string> _mandatoryFullNames;

        public IndexedArgInstances(List<ArgInstance> instances)
        {
            _mandatoryFullNames = new HashSet<string>();
            _argInstancesByName = new MultiDictionary<string, ArgInstance>();
            HashSet<string> tempNames = new HashSet<string>();

            foreach (ArgInstance currentInstance in instances)
            {
                if (currentInstance.Arg.IsMandatory)
                {
                    _mandatoryFullNames.Add(currentInstance.FullName);
                }
                tempNames.Clear();
                tempNames.Add(currentInstance.Name);
                tempNames.Add(currentInstance.FullName);
                if (currentInstance.ShortName != null)
                {
                    tempNames.Add(currentInstance.ShortName);
                }

                foreach (string name in tempNames)
                {
                    _argInstancesByName.Add(name, currentInstance);
                }
            }
        }

        public HashSet<string> MandatoryFullNames
        {
            get
            {
                return _mandatoryFullNames;
            }
        }

        public List<string> GetAllArgumentNames()
        {
            List<string> names = new List<string>(_argInstancesByName.Keys);
            names.Sort();
            return names;
        }

        public List<ArgInstance> GetArgInstancesForName(string argumentName)
        {
            if (!_argInstancesByName.ContainsKey(argumentName))
            {
                return new List<ArgInstance>();
            }

            List<ArgInstance> instances = _argInstancesByName[argumentName];
            return new List<ArgInstance>(instances);
        }
    }
}
