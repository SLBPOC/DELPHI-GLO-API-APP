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
        //public Task<EventDto> CreateAsync(EventDto item)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EventDto> CreateAsyncAlertCustom(EventDto item)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> DeleteAsyncAlertCustom(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> ExistsAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> ExistsAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EventDto> GetAlertCustomByAlertId(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IEnumerable<EventDto>> GetAllListByJson()
        //{
        //    var items = new List<EventDto>();
        //    using (StreamReader r = new StreamReader("JSON/Event.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        items = JsonConvert.DeserializeObject<List<EventDto>>(json);
        //    }
        //    return items;
        //}


        //public async Task<IEnumerable<EventDto>> GetEvents(int page, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate, string? eventType, string? eventStatus)
        //{

        
        //}

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
        //public async Task<IEnumerable<EventDto>> GetAllAsync()
        //{
        //    var events = _dbUnit.events.GetAll().ToList();
        //    var eventsDto = new List<EventDto>();
        //    foreach (var item in events)
        //    {
        //        var eventDto = new EventDto();
        //        eventDto.Id = item.Id;
        //        eventDto.WellName = item.WellName;
        //        eventDto.EventType = item.EventType;
        //        eventDto.EventStatus = item.EventStatus;
        //        eventDto.EventDescription = item.EventDescription;
        //        eventDto.CreationDateTime = item.CreationDateTime;
        //        eventDto.Priority = item.Priority;
        //        eventsDto.Add(eventDto);
        //    }
        //    return eventsDto;
        //}



        //public async Task<EventDto> GetAsync(int id)
        //{
        //    Event events = _dbUnit.events.FirstOrDefault(e => e.Id == id);

        //    if (events == null) return null;

        //    var eventDto = new EventDto();
        //    eventDto.Id = events.Id;
        //    eventDto.WellName = events.WellName;
        //    eventDto.EventType = events.EventType;
        //    eventDto.EventStatus = events.EventStatus;
        //    eventDto.EventDescription = events.EventDescription;

        //    eventDto.Priority = events.Priority;
        //    return eventDto;
        //}

        //public Task<IEnumerable<EventDto>> GetFromJsonFile()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EventDto> UpdateAsync(Guid id, EventDto item)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<EventDto> UpdateAsync(int id, EventDto item)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<EventDto>> GetEvents(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
