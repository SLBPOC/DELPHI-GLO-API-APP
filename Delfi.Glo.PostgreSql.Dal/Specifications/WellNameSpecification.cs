using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class WellNameSpecification : Specification<WellDto>
    {
        public override Expression<Func<WellDto, bool>> ToExpression()
        {
            return m =>  m.WellName.Contains("") ;
            //return wells => wells.WellName.ToLower().Contains(searchstring)
        }
        
    }
}
