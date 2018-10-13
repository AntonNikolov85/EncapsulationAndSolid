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
        public string WorkingDirectory { get; set; }

        public void Save(int id, string message)
        {
            var path = this.GetFileName(id);
            File.WriteAllText(path, message);
        }

        public string Read(int id)
        {
            var path = this.GetFileName(id);
            var msg = File.ReadAllText(path);
            return msg;
        }

        public string GetFileName(int id)
        {
            return Path.Combine(this.WorkingDirectory, id + ".txt");
        }
    }
}
