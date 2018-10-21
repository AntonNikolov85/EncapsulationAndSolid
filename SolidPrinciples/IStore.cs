using System.IO;

namespace SolidPrinciples
{
    public interface IStore
    {
        FileInfo GetFileInfo(int id, string workingDirectory);
        string ReadAllText(string path);
        void WriteAllText(string path, string message);
    }
}