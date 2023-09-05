using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Migrations;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using Newtonsoft.Json;

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


        public async Task<IEnumerable<EventDto>> GetEvents(int page, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate)
        {

            var eventInJson = UtilityService.Read<List<EventDto>>
                                                    (JsonFiles.events).AsQueryable();

            //universitiesInJson = universitiesInJson.Search(v => v.Name, v => v.Country, v => v.AlphaTwoCode
            //    ).Containing(universityByName);

            if (searchString != null)
            {
                var spec = new EventsSpecification(searchString);

                var events = eventInJson.Where(spec.ToExpression());

                if (startDate != null && endDate != null)
                {
                    events = events.Where(c => c.CreationDateTime >= startDate && c.CreationDateTime <= endDate);
                }

                events = DynamicSort.ApplyDynamicSort(events, sortExpression);
                var result = events.Skip((page-1)* pageSize).Take(pageSize).ToList();

                return result;
            }
            else
            {

             
                var  events = DynamicSort.ApplyDynamicSort(eventInJson, sortExpression);
                if (startDate != null && endDate != null)
                {
                    events = events.Where(c => c.CreationDateTime >= startDate && c.CreationDateTime <= endDate);
                }
                var result = events.Skip((page-1) * pageSize).Take(pageSize).ToList();

                return result;
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
