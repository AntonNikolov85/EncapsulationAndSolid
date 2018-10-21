using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class FileStore : IStore
    {
        private readonly DirectoryInfo workingDirectory;

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

            this.workingDirectory = workingDirectory;
        }
        public virtual void WriteAllText(int id, string message)
        {
            string path = GetFileInfo(id).FullName;
            File.WriteAllText(path, message);
        }

        public virtual string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public virtual FileInfo GetFileInfo(int id)
        {
            return new FileInfo(Path.Combine(this.workingDirectory.FullName, id + ".txt"));
        } 
    }
}
