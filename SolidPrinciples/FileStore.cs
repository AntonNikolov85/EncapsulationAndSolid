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
    public class FileStore
    {
        public FileStore(DirectoryInfo workingDirectory)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException("workingDirectory");
            }
            if (!Directory.Exists(workingDirectory.Name))
            {
                throw new ArgumentException("Non existing directory", "workingDirectory");
            }

            this.Cache = new ConcurrentDictionary<int, string>();
            this.WorkingDirectory = workingDirectory;
        }

        public ConcurrentDictionary<int, string> Cache { get; private set; }

        public DirectoryInfo WorkingDirectory { get; private set; }

        public void Save(int id, string message)
        {
            Log.Information("Saving message {id}.", id);
            var file = this.GetFileInfo(id, this.WorkingDirectory.FullName);
            File.WriteAllText(file.FullName, message);
            this.Cache.AddOrUpdate(id, message, (i,s) => message);
            Log.Information("Saved message {id}.", id);
        }

        public Maybe<string> Read(int id)
        {
            Log.Debug("Reading message {id}.", id);
            var file = this.GetFileInfo(id, this.WorkingDirectory.FullName);
            if (!file.Exists)
            {
                Log.Debug("No message {id} found.", id);
                return new Maybe<string>();
            }
            var message = this.Cache.GetOrAdd(id, _ => File.ReadAllText(file.FullName));
            Log.Debug("Returing message {id}.", id);
            return new Maybe<string>(message);
        }

        public FileInfo GetFileInfo(int id, string workingDirectory)
        {
            return new FileInfo(Path.Combine(workingDirectory, id + ".txt"));
        }
    }
}
