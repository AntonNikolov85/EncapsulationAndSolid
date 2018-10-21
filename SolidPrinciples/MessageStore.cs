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
        private readonly StoreLogger logger;
        private readonly StoreCache cache;
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
            this.logger.Saving(id);
            this.Store.WriteAllText(id, message);
            this.cache.AddOrUpdate(id, message);
            this.logger.Saved(id);
        }

        public Maybe<string> Read(int id)
        {
            this.logger.Reading(id);
            var file = this.Store.GetFileInfo(id);
            if (!file.Exists)
            {
                this.logger.DidNotFind(id);
                return new Maybe<string>();
            }
            var message = this.cache.GetOrAdd(id, _ => this.Store.ReadAllText(file.FullName));
            this.logger.Returning(id);
            return new Maybe<string>(message);
        }

        protected virtual StoreLogger Logger
        {
            get { return this.logger; }
        }

        protected virtual StoreCache Cache
        {
            get { return this.cache; }
        }

        protected virtual IStore Store
        {
            get { return this.store; }
        }
    }
}
