using System.IO;

namespace SolidPrinciples
{
    public interface IStore
    {
        Maybe<string> ReadAllText(int id);
    }
}