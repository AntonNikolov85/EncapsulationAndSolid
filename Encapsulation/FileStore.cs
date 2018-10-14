using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    public class FileStore
    {
        public FileStore(string workingDirectory)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException("workingDirectory");
            }
            if (!Directory.Exists(workingDirectory))
            {
                throw new ArgumentException("Non existing directory", "workingDirectory");
            }

            this.WorkingDirectory = workingDirectory;
        }

        public string WorkingDirectory { get; private set; }

        public void Save(int id, string message)
        {
            var path = this.GetFileName(id);
            File.WriteAllText(path, message);
        }

        public string Read(int id)
        {
            var path = this.GetFileName(id);
            if (!File.Exists(path))
            {
                throw new ArgumentException("there s no file with that id", "id");
            }
            var msg = File.ReadAllText(path);
            return msg;
        }

        public string GetFileName(int id)
        {
            return Path.Combine(this.WorkingDirectory, id + ".txt");
        }
    }
}
