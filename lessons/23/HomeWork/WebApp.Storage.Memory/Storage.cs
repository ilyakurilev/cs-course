using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Storage;

namespace WebApp.CityStorage.Memory
{
    public class Storage<T> :IStorage<T>
        where T : IIdentified
    {
        protected readonly List<T> _list;

        public int Count => _list.Count;

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

        public T[] GetItemsOnPage(int page, int perPage)
        {
            return _list.Skip((page - 1) * perPage).Take(perPage).ToArray();
        }
    }
}
