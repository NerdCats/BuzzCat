using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BuzzCatBlind.Utilities
{
    public class ConnectionMapping<T>
    {
        private ConcurrentDictionary<T, HashSet<string>> connections =
            new ConcurrentDictionary<T, HashSet<string>>();

        public int Count { get { return connections.Count; } }

        public bool Add(T key, string connectionId)
        {
            HashSet<string> connSet;
            if (!connections.TryGetValue(key, out connSet))
            {
                connSet = new HashSet<string>();
            }
            lock (connSet)
            {
                connSet.Add(connectionId);
            }

            return connections.TryAdd(key, connSet);
        }

        public bool Remove(T key, string connectionId)
        {
            HashSet<string> connSet;
            if (!connections.TryGetValue(key, out connSet))
            {
                return false;
            }
            lock (connSet)
            {
                connSet.Remove(connectionId);
            }
            if (connSet.Count == 0)
            {
                return connections.TryRemove(key, out connSet);
            }
            return true;
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connSet;
            if (connections.TryGetValue(key, out connSet))
            {
                return connSet;
            }
            
            return Enumerable.Empty<string>();
        }
    }
}
