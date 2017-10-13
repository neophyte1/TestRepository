﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class InMemoryRepository<T> : IRepository<T> where T :IStoreable
    {
        private List<T> _store;

        public InMemoryRepository()
        {
            _store = new List<T>();
        }
        public IEnumerable<T> All()
        {
            return _store;
        }
        

        public void Delete(IComparable id)
        {
            throw new NotImplementedException();
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
            return _store.Find(x => x.Id.Equals(id));
        }

    }
}
