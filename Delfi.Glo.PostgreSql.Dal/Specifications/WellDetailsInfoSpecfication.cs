using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

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
