namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;

    public class MultiDictionary<Key, Value>
    {
        private readonly Dictionary<Key, List<Value>> _dict;

        public MultiDictionary()
        {
            _dict = new Dictionary<Key, List<Value>>();
        }

        public int Count
        {
            get
            {
                return _dict.Count;
            }
        }

        public bool ContainsKey(Key key)
        {
            return _dict.ContainsKey(key);
        }

        public ICollection<Key> Keys
        {
            get
            {
                return _dict.Keys;
            }
        }

        public void Add(Key key, Value value)
        {
            if (!_dict.ContainsKey(key))
            {
                _dict[key] = new List<Value>();
            }
            _dict[key].Add(value);
        }

        public List<Value> this[Key key]
        {
            get
            {
                return _dict[key];
            }
        }
    }
}
