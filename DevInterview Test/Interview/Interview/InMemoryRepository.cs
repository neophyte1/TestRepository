using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class InMemoryRepository<T> : IRepository<T> where T :IStoreable
    {
        private readonly List<T> _store;

        public InMemoryRepository()
        {
            _store = new List<T>();
        }
        public IEnumerable<T> All()
        {
            if (_store == null)
            {
                throw new NullReferenceException();
            }

            return _store;
        }
        

        public void Delete(IComparable id)
        {
            if (_store == null || _store.Count == 0)
            {
                throw new Exception("Items should exist in the list before you invoke Delete().");

            }

            _store.RemoveAll(x => x.Id.Equals(id));

        }

        public void Save(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "Argument cannot be null.");

            }

            if (!_store.Contains(item))
            {
                _store.Add(item);
            }

        }

        public T FindById(IComparable id)
        {
            if (_store == null || _store.Count == 0)
            {
                throw new Exception("Items required in the List.");

            }

            return _store.Find(x => x.Id.Equals(id));
        }

    }
}
