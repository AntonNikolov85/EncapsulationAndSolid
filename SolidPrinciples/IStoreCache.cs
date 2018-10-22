using System;

namespace SolidPrinciples
{
    public interface IStoreCache
    {
        void AddOrUpdate(int id, string message);
        Maybe<string> GetOrAdd(int id, Func<int, Maybe<string>> messageFactory);
    }
}