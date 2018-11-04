using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class StoreCache : IStoreWriter, IStoreReader
    {
        private readonly ConcurrentDictionary<int, Maybe<string>> cache;
        private readonly IStoreWriter writer;
        private readonly IStoreReader reader;

        public StoreCache(IStoreWriter writer, IStoreReader reader)
        {
            this.cache = new ConcurrentDictionary<int, Maybe<string>>();
            this.writer = writer;
            this.reader = reader;
        }

        public void Save(int id, string message)
        {
            this.writer.Save(id, message);
            Maybe<string> msg = new Maybe<string>(message);
            this.cache.AddOrUpdate(id, msg, (i, s) => msg);
        }

        public Maybe<string> Read(int id)
        {
            Maybe<string> returnValue;
            if (this.cache.TryGetValue(id,out returnValue))
            {
                return returnValue;
            }

            returnValue = this.reader.Read(id);
            if (returnValue.Any())
            {
                this.cache.AddOrUpdate(id, returnValue, (i, r) => returnValue);
            }

            return returnValue;
        }
    }
}
