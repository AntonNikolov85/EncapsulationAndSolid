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
        private readonly IStoreWriter writer;
        private readonly IStoreReader reader;
        private readonly IFileLocator fileLocator;

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
            var fileStore = new FileStore(workingDirectory);
            var c = new StoreCache(fileStore, fileStore);
            var l = new StoreLogger(c, c);
            this.logger = l;
            this.cache = c;
            this.store = fileStore;
            this.fileLocator = fileStore;
            this.writer = l;
            this.reader = l;
        }

        public DirectoryInfo WorkingDirectory { get; private set; }

        public void Save(int id, string message)
        {
            this.Writer.Save(id, message);
        }

        public Maybe<string> Read(int id)
        {
            this.Logger.Reading(id);
            Maybe<string> message = this.Cache.Read(id);
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

        public FileInfo GetFileInfo(int id)
        {
            return new FileInfo(Path.Combine(this.WorkingDirectory.FullName, id + ".txt"));
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

        public virtual IStoreWriter Writer
        {
            get { return this.writer; }
        }

        public virtual IStoreReader Reader
        {
            get { return this.reader; }
        }

        public virtual IFileLocator FileLocator
        {
            get { return this.fileLocator; }
        }
    }
}
