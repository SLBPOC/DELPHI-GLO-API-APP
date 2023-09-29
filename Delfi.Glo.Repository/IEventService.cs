using Delfi.Glo.Common.Services;

namespace Delfi.Glo.Repository
{

    public interface IEventService<T> where T : class
    {
        Task<Tuple<bool, IEnumerable<T>, int>> GetEvents(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate, string? eventType, string? eventStatus);
    }
}
