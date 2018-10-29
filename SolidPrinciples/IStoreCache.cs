using System;

namespace SolidPrinciples
{
    public interface IStoreCache
    {
        Maybe<string> GetOrAdd(int id, Func<int, Maybe<string>> messageFactory);
    }
}