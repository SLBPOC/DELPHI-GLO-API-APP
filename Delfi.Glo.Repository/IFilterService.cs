using Delfi.Glo.Common.Services;

namespace Delfi.Glo.Repository
{
    public interface IFilterService<T> where T : class
    {
        Task<Tuple<bool, IEnumerable<T>, int, int, int, int>> GetListByFilter(int pageIndex, int pageSize, string? searchString, string? ApprovalStatus, string? ApprovalMode, List<SortExpression> sortExpression);

    }
}
