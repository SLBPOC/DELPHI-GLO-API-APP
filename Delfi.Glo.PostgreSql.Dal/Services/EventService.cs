using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class EventService : IEventService<EventDto>
    {
        private readonly DbUnitWork _dbUnit;

        public EventService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
        async Task<Tuple<bool, IEnumerable<EventDto>, int>> IEventService<EventDto>.GetEvents(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate, string? eventType, string? eventStatus)
        {
            var eventInJson = UtilityService.Read<List<EventDto>> (JsonFiles.Events).AsQueryable();

            ///get only last 7 days data
            DateTime lastDate = DateTime.Now;
            DateTime FirstDate = lastDate.AddDays(-7);
            eventInJson = eventInJson.Where(r => r.CreationDateTime >= FirstDate && r.CreationDateTime <= lastDate);


            int Count = 0;
            Count=eventInJson.Count();


            if (searchString != null)
            {
                var search = searchString.ToLower();
                var spec = new EventsSpecification(search);

                var events = eventInJson.Where(spec.ToExpression());

                if (startDate != null && endDate != null)
                {
                    events = events.Where(c => c.CreationDateTime.Value.Year >= startDate.Value.Year
                                            && c.CreationDateTime.Value.Month >= startDate.Value.Month
                                            && c.CreationDateTime.Value.Day >= startDate.Value.Day

                  && c.CreationDateTime.Value.Year <= endDate.Value.Year
                                            && c.CreationDateTime.Value.Month <= endDate.Value.Month
                                            && c.CreationDateTime.Value.Day <= endDate.Value.Day);

                    Count = events.Count();
                }
                if (eventType != null && eventStatus == null)
                {
                    events = events.Where(c => c.EventType == eventType);
                    Count = events.Count();
                }
                if (eventType == null && eventStatus != null)
                {
                    events = events.Where(c => c.EventStatus == eventStatus);
                    Count = events.Count();
                }
                if (eventType != null && eventStatus != null)
                {
                    events = events.Where(c => c.EventType == eventType && c.EventStatus == eventStatus);
                    Count = events.Count();
                }

                events = DynamicSort.ApplyDynamicSort(events, sortExpression);
                var result = events.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                Count = events.Count();
                return await  Task.FromResult(new Tuple<bool, IEnumerable<EventDto>, int>(true, result, Count));
            }
            else
            {


                var events = DynamicSort.ApplyDynamicSort(eventInJson, sortExpression);
                if (startDate != null && endDate != null)
                {
                    events = events.Where(c => c.CreationDateTime.Value.Year >= startDate.Value.Year
                                            && c.CreationDateTime.Value.Month >= startDate.Value.Month
                                            && c.CreationDateTime.Value.Day >= startDate.Value.Day

                  && c.CreationDateTime.Value.Year <= endDate.Value.Year
                                            && c.CreationDateTime.Value.Month <= endDate.Value.Month
                                            && c.CreationDateTime.Value.Day <= endDate.Value.Day);
                    Count = events.Count();
                }

                if (eventType != null && eventStatus == null)
                {
                    events = events.Where(c => c.EventType == eventType);
                    Count = events.Count();
                }
                if (eventType == null && eventStatus != null)
                {
                    events = events.Where(c => c.EventStatus == eventStatus);
                    Count = events.Count();
                }
                if (eventType != null && eventStatus != null)
                {
                    events = events.Where(c => c.EventType == eventType && c.EventStatus == eventStatus);
                    Count = events.Count();
                }
                var result = events.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                Count = events.Count();

                return await  Task.FromResult(new Tuple<bool, IEnumerable<EventDto>, int>(true, result, Count));
               
            }
        }
       
    }
}
