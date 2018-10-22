using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class StoreCache : IStoreCache
    {
        private readonly ConcurrentDictionary<int, Maybe<string>> cache;

        public StoreCache()
        {
            this.cache = new ConcurrentDictionary<int, Maybe<string>>();
        }

        public virtual void AddOrUpdate(int id, string message)
        {
            Maybe<string> msg = new Maybe<string>(message);
            this.cache.AddOrUpdate(id, msg, (i, s) => msg);
        }

        public virtual Maybe<string> GetOrAdd(int id, Func<int, Maybe<string>> messageFactory)
        {
            return this.cache.GetOrAdd(id, i => messageFactory(i));
        }
    }
}
