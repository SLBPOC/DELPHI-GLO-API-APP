using Delfi.Glo.Common.Services;

namespace Delfi.Glo.Repository
{
    public interface IUniversityService<T> where T : class
    {
        Task<IEnumerable<T>> GetUniversities(int pageIndex, int pageSize, string? universityByName,List<SortExpression> sortExpression);
    }
}
