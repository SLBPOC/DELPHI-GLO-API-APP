using Ardalis.GuardClauses;
using Delfi.Glo.Api.Exceptions;
using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Printing;


namespace Delfi.Glo.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : Controller
    {
        private readonly ILogger<AlertsController> _logger;
        private readonly IAlertService<AlertsDto> _alertsService;
        public AlertsController(ILogger<AlertsController> logger,IAlertService<AlertsDto> alertService)
        {
            _logger = logger;
            _alertsService = alertService;

        }
    

        [HttpPost("GetAlertsList")]
        public async Task<IActionResult> Get(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate)
        {
            Guard.Against.InvalidPageIndex(pageIndex);
            Guard.Against.InvalidPageSize(pageSize);

            var alertInJson = UtilityService.Read<List<AlertsDto>>
                                              (JsonFiles.Alerts).AsQueryable();
            Tuple<IEnumerable<AlertsDto>, int> values = await _alertsService.GetAlerts(pageIndex, pageSize, searchString, sortExpression, startDate, endDate);

             if (values.Item1 != null && values.Item1?.Count() > 0) return Ok(JsonConvert.SerializeObject(new { success = true, data = values.Item1, totalCount = values.Item2 }));
            else return NotFound($"No Alert found with name {searchString}");
        }

        [HttpPost("GetSnoozeByAlert")]
        public async Task<ActionResult> GetSnoozeByAlert(int alertId, int snoozeBy)
        {
            int Count = 0;
            int High = 0;
            int Medium = 0;
            int Low = 0;
            int Cleared = 0;
            var alertsList = UtilityService.Read<List<AlertsDto>>
                                      (JsonFiles.Alerts).AsQueryable();
            var result = await _alertsService.GetSnoozeByAlert(alertId, snoozeBy);
            if(result!=null && result?.Count() > 0) {
                Count = alertsList.Count();
                High = result.Where(a => a.AlertLevel == "High").Count();
                Medium = result.Where(a => a.AlertLevel == "Medium").Count();
                Low = result.Where(a => a.AlertLevel == "Low").Count();
                Cleared = result.Where(a => a.AlertLevel == "Cleared").Count();
            }
         
            if (result != null && result?.Count() > 0) return Ok(JsonConvert.SerializeObject(new { success = true, data = result, totalCount = Count, totalHigh = High, totalMedium = Medium, totalLow = Low, totalCleared = Cleared }));
            else return NotFound($"No Alert for Snooze by {snoozeBy}");
        }

        [HttpPost("ClearAlert")]
        public async Task<ActionResult> SetClearAlert(int alertId, string comment)
        {
            var result= _alertsService.SetClearAlert(alertId,comment);
            if (result != null ) return Ok(JsonConvert.SerializeObject(new { success = true, data = result }));
            else return NotFound($"No Alert for Clear by {alertId}");
        }
    }
}
