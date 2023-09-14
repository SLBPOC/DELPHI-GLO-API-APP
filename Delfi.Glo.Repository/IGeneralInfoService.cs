namespace Delfi.Glo.Repository
{
    public interface IGeneralInfoService<T> where T : class
    {
        Task<T> GetAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
      
    }
}
