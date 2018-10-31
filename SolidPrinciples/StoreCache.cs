using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class StoreCache : IStoreCache, IStoreWriter
    {
        private readonly ConcurrentDictionary<int, Maybe<string>> cache;
        private readonly IStoreWriter writer;

        public StoreCache(IStoreWriter writer)
        {
            this.cache = new ConcurrentDictionary<int, Maybe<string>>();
            this.writer = writer;
        }

        public virtual void Save(int id, string message)
        {
            this.writer.Save(id, message);
            Maybe<string> msg = new Maybe<string>(message);
            this.cache.AddOrUpdate(id, msg, (i, s) => msg);
        }

        public virtual Maybe<string> GetOrAdd(int id, Func<int, Maybe<string>> messageFactory)
        {
            return this.cache.GetOrAdd(id, i => messageFactory(i));
        }
    }
}
