using System;

namespace WebApp.Storage
{
    public interface IStorage<T>
        where T : IIdentified
    {
        T[] List();
        void Add(T item);
        T Get(Guid id);
        void Update(T item);
        void Remove(T item);
    }
}
