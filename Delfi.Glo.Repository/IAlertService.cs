using Delfi.Glo.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
    public interface IAlertService<T> where T : class
    {
        Task<IEnumerable<T>> GetAlerts(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate);

        Task<IEnumerable<T>> GetSnoozeByAlert(int alertId, int snoozeBy);
        Task<bool> SetClearAlert(int alertId, string comment);
    }
}
