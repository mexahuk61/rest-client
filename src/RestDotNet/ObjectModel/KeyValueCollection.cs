using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RestDotNet.ObjectModel
{
    internal class KeyValueCollection<TKey, TValue> : Collection<KeyValuePair<TKey, TValue>>
    {
        public void Add(TKey key, TValue value)
        {
            this.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Any(pair => pair.Key.Equals(key));
        }

        public IEnumerable<TValue> GetValues(TKey key)
        {
            return this.Where(pair => pair.Key.Equals(key)).Select(pair => pair.Value);
        }
    }
}