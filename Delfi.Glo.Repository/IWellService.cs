using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
    public interface IWellService<T> where T : class
    {
        Task<T> GetWellDetailsInfoById(int WellId);

      //  Task<T> GetSetPointDetailsByTotalUpTime(int WellId, DateTime StartDate, DateTime EndDate);
    }
}
