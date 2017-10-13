using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public interface IWriteRepository<T> where T : IStoreable
    {
       void Delete(IComparable id);
        void Save(T item);
    }

}
