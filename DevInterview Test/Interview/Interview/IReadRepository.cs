using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public interface IReadRepository<T> where T : IStoreable
    {
        IEnumerable<T> All();

        T FindById(IComparable id);
    }
}
