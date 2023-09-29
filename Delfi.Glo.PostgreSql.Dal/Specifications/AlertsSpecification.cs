using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class AlertsSpecification : Specification<AlertsDto>
    {
        private readonly string searchstring;
        public AlertsSpecification(string searchstring)
        {
            this.searchstring = searchstring;
        }
        public override Expression<Func<AlertsDto, bool>> ToExpression()
        {
            return evetns => evetns.WellName.ToLower().Contains(searchstring) || evetns.AlertStatus.ToLower().Contains(searchstring) || evetns.AlertType.ToLower().Contains(searchstring) || evetns.AlertDescription.ToLower().Contains(searchstring);

        }
    }
}
