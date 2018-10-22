using System.IO;

namespace SolidPrinciples
{
    public interface IStore
    {
        Maybe<string> ReadAllText(int id);
        void WriteAllText(int id, string message);
    }
}