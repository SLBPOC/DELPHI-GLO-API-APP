﻿using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;

namespace Delfi.Glo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly ICrudService<EventDto> _eventService;
        public EventController(ILogger<EventController> logger, ICrudService<EventDto> eventService)
        {
            _logger = logger;
            _eventService = eventService;

        }
        [HttpGet()]
        public async Task<IEnumerable<EventDto>> Get()
        {

            return await _eventService.GetAllAsync();
        }

        [HttpGet("Id")]
        public async Task<ActionResult<EventDto>> Get(int id)
        {


            return await _eventService.GetAsync(id);
        }
        [HttpGet("GetAllEventListByJson")]
        public async Task<IEnumerable<EventDto>> GetAllEventListByJson()
        {
            var items = new List<EventDto>();
            using (StreamReader r = new StreamReader("JSON/Event.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<EventDto>>(json);
            }
            return items;
        }

        [HttpPost("GetEventList")]

        public async Task<ActionResult> GetEventistByFilters(SearchCreteria creteria, DateTime? startDate, DateTime? endDate, string? eventType, string? eventStatus)
        {
            var eventsDto = new List<EventDto>();

            int Count = 0;

            var events = await GetAllEventListByJson();

            if (events != null)
            {
                if (startDate != null && endDate != null)
                    events = events.Where(c => c.CreationDateTime >= startDate && c.CreationDateTime <= endDate);
                if (!string.IsNullOrEmpty(eventType))
                    events = events.Where(x => x.EventType == eventType);
                if (!string.IsNullOrEmpty(eventStatus))
                    events = events.Where(x => x.EventStatus == eventStatus);
                Count = events.Count();

                if (creteria != null)
                {
                    var searchevents = events;
                    if (creteria.searchString.Length > 0)
                    {
                        searchevents = events.Where(a => a.WellName.Contains(creteria.searchString) || a.EventStatus.Contains(creteria.searchString) || a.EventType.Contains(creteria.searchString) || a.EventDescription.Contains(creteria.searchString)).ToList();
                        Count = searchevents.Count();

                    }

                    if (creteria.Status.Length > 0)
                    {
                        searchevents = searchevents.Where(a => a.EventStatus == creteria.Status).ToList();
                        events = searchevents;
                    }

                    if (creteria.field != null && creteria.field != "" && creteria.field != null && creteria.dir != "")
                    {
                        //Sorting enabled
                        if (events.Count() > creteria.pageSize)
                        {
                            if (creteria.dir == "asc")
                            {
                                events = searchevents.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                            }
                            else
                            {
                                events = searchevents.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                            }

                        }
                        else
                        {
                            if (creteria.dir == "asc")
                            {
                                events = searchevents.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
                            }
                            else
                            {
                                events = searchevents.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
                            }
                        }
                    }
                    else
                    {
                        //Default , no sorting enabled
                        if (events.Count() > creteria.pageSize)
                        {
                            events = searchevents.Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                        }
                        else
                        {
                            events = searchevents.Take(creteria.pageSize).ToList();
                        }
                    }
                }
                foreach (var item in events)
                {
                    var eventDto = new EventDto();
                    eventDto.WellName = item.WellName;
                    eventDto.EventStatus = item.EventStatus;
                    eventDto.EventType = item.EventType;
                    eventDto.EventDescription = item.EventDescription;
                    eventDto.Priority = item.Priority;
                    eventDto.CreationDateTime = item.CreationDateTime;
                    eventsDto.Add(eventDto);
                }

            }

            return Ok(JsonConvert.SerializeObject(new { success = true, data = eventsDto, totalCount = Count }));

        }
    }
}

