using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Storage.Memory
{
    public class Storage<T> :IStorage<T>
        where T : IIdentified
    {
        private readonly List<T> _list;

        public Storage()
        {
            _list = new List<T>();
        }

        public Storage(params T[] items)
        {
            _list = items.ToList();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public T Get(Guid id)
        {
            return _list.FirstOrDefault(_ => _.Id == id);
        }

        public T[] List()
        {
            return _list.ToArray();
        }

        public void Remove(T item)
        {
            _list.Remove(item);
        }

        public void Update(T item)
        {
            var itemInList = Get(item.Id);

            if (itemInList != null)
            {
                _list.Remove(item);
                _list.Add(item);
            }
        }
    }
}
