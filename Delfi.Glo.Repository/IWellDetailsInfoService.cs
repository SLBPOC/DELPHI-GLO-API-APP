using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
  
        public interface IWellDetailsInfoService<T> where T : class
    { 
        Task<T> GetSwimLaneDetailsByDate(int WellId, DateTime StartDate, DateTime EndDate);
    }
}
