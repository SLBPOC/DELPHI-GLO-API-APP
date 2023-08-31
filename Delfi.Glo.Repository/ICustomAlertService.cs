using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
    public interface ICustomAlertService<T> where T : class
    {
        Task<IEnumerable<T>> GetFromJsonFile();
        Task<IEnumerable<T>> CreateAsyncAlertCustom(T item);
        Task<bool> DeleteAsyncAlertCustom(int id);
        Task<bool> UpdateAsyncAlertCustom(int id, bool check);
        Task<T> GetAlertCustomByAlertId(int id);
    }
}
