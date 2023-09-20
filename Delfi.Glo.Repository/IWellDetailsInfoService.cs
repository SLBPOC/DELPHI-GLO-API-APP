
using Delfi.Glo.Entities.Dto;

namespace Delfi.Glo.Repository
{
  
    public interface IWellDetailsInfoService<T> where T : class
    {
        Task<T> GetWellDetailsInfoById(int WellId);
        Task<SwimLaneGraphDetails> GetSwimLaneDetailsByDate(int WellId, DateTime StartDate, DateTime EndDate);

    }
}
