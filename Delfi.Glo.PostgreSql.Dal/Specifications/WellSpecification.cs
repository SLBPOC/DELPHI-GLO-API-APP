using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
       public sealed class WellSpecification : Specification<WellDto>
    {
        private readonly string searchstring;
        public WellSpecification(string searchstring)
        {
            this.searchstring = searchstring;
        }
        public override Expression<Func<WellDto, bool>> ToExpression()
        {
            return wells => wells.WellName.ToLower().Contains(searchstring)
                            || wells.WellPriority.ToLower().Contains(searchstring)
                            || wells.CompressorUpTime.ToString().Contains(searchstring)
                            || wells.ProductionUpTime.ToString().Contains(searchstring)
                            || wells.DeviceUpTime.ToString().Contains(searchstring)
                            || wells.GLISetPoint.ToString().Contains(searchstring)
                            || wells.QLiq.ToString().Contains(searchstring)
                            || wells.QOil.ToString().Contains(searchstring)
                            || wells.Qg.ToString().Contains(searchstring)
                            || wells.Qw.ToString().Contains(searchstring)
                            || wells.Wc.ToString().Contains(searchstring)
                            || wells.CurrentGLISetpoint.ToString().Contains(searchstring)
                            || wells.CurrentCycleStatus.ToLower().Contains(searchstring)
                            || wells.ApprovalMode.ToLower().Contains(searchstring)
                            || wells.ApprovalStatus.ToLower().Contains(searchstring);
        }
    }
}
