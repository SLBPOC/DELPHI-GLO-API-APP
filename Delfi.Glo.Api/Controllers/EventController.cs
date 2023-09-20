using Ardalis.GuardClauses;
using Delfi.Glo.Api.Exceptions;
using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Delfi.Glo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService<EventDto> _eventService;
        public EventController(ILogger<EventController> logger, IEventService<EventDto> eventService)
        {
            _logger = logger;
            _eventService = eventService;

        }
       

        [HttpPost("GetEventList")]
        public async Task<IActionResult> Get(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate,string ?eventType,string ? eventStatus)
        {
            Guard.Against.InvalidPageIndex(pageIndex);
            Guard.Against.InvalidPageSize(pageSize);

            Tuple<bool, IEnumerable<EventDto>, int> result = await _eventService.GetEvents(pageIndex, pageSize, searchString, sortExpression,startDate,endDate,eventType,eventStatus);
            //var eventInJson = UtilityService.Read<List<EventDto>>
            //                                     (JsonFiles.events).AsQueryable();
            return Ok(JsonConvert.SerializeObject(new { success = result.Item1, data = result.Item2, totalCount = result.Item3 }));
            //if (result != null && result?.Count() > 0) return Ok(JsonConvert.SerializeObject(new { success = true, data = result, totalCount = Count }));
            //else return NotFound($"No Event found with name {searchString}");
        }
     
    }
}

