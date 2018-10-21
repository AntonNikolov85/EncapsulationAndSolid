using System.IO;

namespace SolidPrinciples
{
    public interface IStore
    {
        FileInfo GetFileInfo(int id);
        string ReadAllText(string path);
        void WriteAllText(int id, string message);
    }
}