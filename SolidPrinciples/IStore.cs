using System.IO;

namespace SolidPrinciples
{
    public interface IStore
    {
        void Save(int id, string message);
        Maybe<string> Read(int id);
    }
}