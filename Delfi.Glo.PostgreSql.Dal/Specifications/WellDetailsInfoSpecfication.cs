using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class WellDetailsInfoSpecfication : Specification<WellInfoByRangeDto>
    {
        private readonly int wellId;
        public WellDetailsInfoSpecfication(int wellId)
        {
            this.wellId = wellId;
        }
        public override Expression<Func<WellInfoByRangeDto, bool>> ToExpression()
        {
            return well => well.Id == wellId;
        }
       
    }
}
