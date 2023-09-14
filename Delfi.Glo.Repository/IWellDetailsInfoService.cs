
namespace Delfi.Glo.Repository
{
  
        public interface IWellDetailsInfoService<T> where T : class
    { 
        Task<T> GetSwimLaneDetailsByDate(int WellId, DateTime StartDate, DateTime EndDate);
    }
}
