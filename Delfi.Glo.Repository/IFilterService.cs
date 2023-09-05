using Delfi.Glo.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
    public interface IFilterService<T> where T : class
    {
        Task<Tuple<bool, IEnumerable<T>, int, int, int, int>> GetListByFilter(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression);

    }
}
