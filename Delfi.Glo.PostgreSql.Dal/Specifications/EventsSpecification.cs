using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System.Linq.Expressions;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class EventsSpecification : Specification<EventDto>
    {
        private readonly string searchstring;
        public EventsSpecification(string searchstring)
        {
            this.searchstring = searchstring;
        }
        public override Expression<Func<EventDto, bool>> ToExpression()
        {
            return evetns => evetns.WellName.ToLower().Contains(searchstring) || evetns.EventStatus.ToLower().Contains(searchstring) || evetns.EventType.ToLower().Contains(searchstring) || evetns.EventDescription.ToLower().Contains(searchstring);
     
        }
    }
}
