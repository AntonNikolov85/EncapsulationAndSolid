namespace SolidPrinciples
{
    public interface IStoreReader
    {
        Maybe<string> Read(int id);
    }
}