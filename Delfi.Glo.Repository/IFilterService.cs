using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
    public interface IFilterService<T1,T2>
    {
        Task<Tuple<bool, IEnumerable<T1>, int, int, int, int>> GetListByFilter(T2 item);
    }
}
