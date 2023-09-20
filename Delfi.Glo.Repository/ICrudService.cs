namespace Delfi.Glo.Repository
{
    public interface ICrudService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(int id, T item);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);    
        //Task<IEnumerable<T>> GetAllListByJson();
        //Task<IEnumerable<T>> GetFromJsonFile();
        Task<IEnumerable<T>> GetWells();

    }
}
