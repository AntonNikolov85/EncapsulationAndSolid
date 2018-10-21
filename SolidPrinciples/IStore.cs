using System.IO;

namespace SolidPrinciples
{
    public interface IStore
    {
        FileInfo GetFileInfo(int id);
        Maybe<string> ReadAllText(int id);
        void WriteAllText(int id, string message);
    }
}