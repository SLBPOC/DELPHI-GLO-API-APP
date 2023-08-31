using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class EventService : ICrudService<EventDto>
    {
        private readonly DbUnitWork _dbUnit;

        public EventService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
        public Task<EventDto> CreateAsync(EventDto item)
        {
            throw new NotImplementedException();
        }

        public Task<EventDto> CreateAsyncAlertCustom(EventDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsyncAlertCustom(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EventDto> GetAlertCustomByAlertId(int id)
        {
            throw new NotImplementedException();
        }
   
        public async Task<IEnumerable<EventDto>> GetAllListByJson()
        {
            var items = new List<EventDto>();
            using (StreamReader r = new StreamReader("JSON/Event.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<EventDto>>(json);
            }
            return items;
        }
        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            var events = _dbUnit.events.GetAll().ToList();
            var eventsDto = new List<EventDto>();
            foreach (var item in events)
            {
                var eventDto = new EventDto();
                eventDto.Id = item.Id;
                eventDto.WellName = item.WellName;
                eventDto.EventType = item.EventType;
                eventDto.EventStatus = item.EventStatus;
                eventDto.EventDescription = item.EventDescription;
                eventDto.CreationDateTime= item.CreationDateTime;
                eventDto.Priority = item.Priority;            
                eventsDto.Add(eventDto);
            }
            return eventsDto;
        }



        public async Task<EventDto> GetAsync(int id)
        {
            Event events=_dbUnit.events.FirstOrDefault(e=>e.Id==id);

            if (events == null) return null;

            var eventDto = new EventDto();
            eventDto.Id = events.Id;
            eventDto.WellName = events.WellName;
            eventDto.EventType = events.EventType;
            eventDto.EventStatus = events.EventStatus;
            eventDto.EventDescription = events.EventDescription;

            eventDto.Priority = events.Priority;
            return eventDto;
        }

        public Task<IEnumerable<EventDto>> GetFromJsonFile()
        {
            throw new NotImplementedException();
        }

        public Task<EventDto> UpdateAsync(Guid id, EventDto item)
        {
            throw new NotImplementedException();
        }

        public Task<EventDto> UpdateAsync(int id, EventDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            throw new NotImplementedException();
        }
    }
}
