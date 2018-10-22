using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class MessageStore
    {
        private readonly IStoreLogger logger;
        private readonly IStoreCache cache;
        private readonly IStore store;

        public MessageStore(DirectoryInfo workingDirectory)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException("workingDirectory");
            }
            if (!Directory.Exists(workingDirectory.Name))
            {
                throw new ArgumentException("Non existing directory", "workingDirectory");
            }

            this.WorkingDirectory = workingDirectory;
            this.logger = new StoreLogger();
            this.cache = new StoreCache();
            this.store = new FileStore(workingDirectory);
        }

        public DirectoryInfo WorkingDirectory { get; private set; }

        public void Save(int id, string message)
        {
            this.Logger.Saving(id, message);
            this.Store.Save(id, message);
            this.Cache.Save(id, message);
            this.Logger.Saved(id, message);
        }

        public Maybe<string> Read(int id)
        {
            this.Logger.Reading(id);
            Maybe<string> message = this.Cache.GetOrAdd(id, _ => this.Store.ReadAllText(id));
            if (message.Any())
            {
                this.Logger.Returning(id);
            }
            else
            {
                this.Logger.DidNotFind(id);
            }
            return message;
        }

        protected virtual IStoreLogger Logger
        {
            get { return this.logger; }
        }

        protected virtual IStoreCache Cache
        {
            get { return this.cache; }
        }

        protected virtual IStore Store
        {
            get { return this.store; }
        }
    }
}
