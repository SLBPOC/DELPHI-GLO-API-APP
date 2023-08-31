namespace Delfi.Glo.Repository
{
    public interface ICrudService<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetFromJsonFile();
        Task<T> CreateAsyncAlertCustom(T item);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(int id, T item);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsyncAlertCustom(int id);        
        Task<bool> ExistsAsync(int id);        
        Task<bool> UpdateAsyncAlertCustom(int id, bool check);
        Task<T> GetAlertCustomByAlertId(int id);

        Task<IEnumerable<T>> GetAllListByJson();

    }
}
