namespace Delfi.Glo.Repository
{
    public interface IGeneralInfoService<T> where T : class
    {
        Task<T> GetWellGeneralInfoAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
      
    }
}
