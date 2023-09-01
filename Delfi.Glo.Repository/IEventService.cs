using Delfi.Glo.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{

    public interface IEventService<T> where T : class
    {
        Task<IEnumerable<T>> GetEvents(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression);
    }
}
