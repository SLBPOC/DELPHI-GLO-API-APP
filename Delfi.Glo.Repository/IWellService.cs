using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;

namespace Delfi.Glo.Repository
{
    public interface IWellService<T> where T : class
    {
        Task<Tuple<bool, IEnumerable<WellDto>, int, int, int, int>> GetWellListByFilter(int page, int pageSize, string? searchString, string? ApprovalStatus, string? ApprovalMode, List<SortExpression> sortExpression);
        Task<IEnumerable<WellDto>> GetAllAsync();
        Task<IEnumerable<WellDto>> GetWells();
        Task<WellDto> GetAsync(int id);
    }
}
