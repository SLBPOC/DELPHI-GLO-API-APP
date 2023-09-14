namespace Delfi.Glo.Repository
{
    public interface ICustomAlertService<T> where T : class
    {
        Task<IEnumerable<T>> GetCustomAlert();
        Task<bool> CreateCustomAlert(T item);
        Task<bool> DeleteCustomAlert(int id);
        Task<bool> UpdateToggle(int id, bool check);
        Task<T> GetCustomAlertByAlertId(int id);
    }
}
