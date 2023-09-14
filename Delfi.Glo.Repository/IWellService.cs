namespace Delfi.Glo.Repository
{
    public interface IWellService<T> where T : class
    {
        Task<T> GetWellDetailsInfoById(int WellId);

      //  Task<T> GetSetPointDetailsByTotalUpTime(int WellId, DateTime StartDate, DateTime EndDate);
    }
}
