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
    
        //[HttpGet("GetAllAlertListByJson")]
        //public async Task<IEnumerable<AlertsDto>> GetAllAlertListByJson()
        //{
        //    var items = new List<AlertsDto>();
        //    using (StreamReader r = new StreamReader("JSON/Alert.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        items = JsonConvert.DeserializeObject<List<AlertsDto>>(json);
        //    }
        //    return items;
        //}
       // [HttpPost("GetAlertsList")]
       // public async Task<ActionResult> GetAlertsListByFilters(SearchCreteria creteria, DateTime? startDate, DateTime? endDate)
       //{
       //     var alertssDto = new List<AlertsDto>();
       //     int Count = 0;

       //     var alerts = await GetAllAlertListByJson();


       //     if (alerts != null)
       //     {
       //         foreach (var alert in alerts.Where(a => a.SnoozeFlag == true))
       //         {
       //             var snInterval = (alert.SnoozeInterval == null || alert.SnoozeInterval == "" ? "0" : alert.SnoozeInterval);
       //             var snFlag = alert.SnoozeFlag;
       //             var snDateTime = alert.SnoozeDateTime;
       //             var s = Convert.ToDateTime(snDateTime).AddHours(Convert.ToInt32(snInterval));
       //             if (s < DateTime.Now)
       //                 alert.SnoozeFlag = false;
       //             else
       //                 alert.SnoozeFlag = true;
       //         }
       //         alerts = alerts.Where(a => a.SnoozeFlag == false);

       //         if (startDate!=null && endDate!=null)
       //         {
       //             alerts = alerts.Where(c => c.TimeandDate >= startDate && c.TimeandDate <= endDate);
       //         }
      

       // Count = alerts.Count();

       //         if (creteria != null)
       //         {
       //             var searchalerts = alerts;
       //             if (creteria.searchString.Length > 0 && alerts.Count()>0)
       //             {
       //                 searchalerts = alerts.Where(a => a.WellName.Contains(creteria.searchString )||a.AlertStatus.Contains(creteria.searchString) || a.AlertType.Contains(creteria.searchString) || a.AlertLevel.Contains(creteria.searchString)|| a.AlertDescription.Contains(creteria.searchString)).ToList();
       //                 Count = searchalerts.Count();

       //             }

       //             if (creteria.Status.Length > 0)
       //             {
       //                 searchalerts = searchalerts.Where(a => a.AlertStatus == creteria.Status).ToList();
       //                 alerts = searchalerts;
       //             }

       //             if (creteria.field != null && creteria.field != "" && creteria.field != null && creteria.dir != "")
       //             {
       //                 //Sorting enabled
       //                 if (alerts.Count() > creteria.pageSize)
       //                 {
       //                     if (creteria.dir == "asc")
       //                     {
       //                         alerts = searchalerts.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
       //                     }
       //                     else
       //                     {
       //                         alerts = searchalerts.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
       //                     }

       //                 }
       //                 else
       //                 {
       //                     if (creteria.dir == "asc")
       //                     {
       //                         alerts = searchalerts.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
       //                     }
       //                     else
       //                     {
       //                         alerts = searchalerts.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
       //                     }
       //                 }
       //             }
       //             else
       //             {
       //                 //Default , no sorting enabled
       //                 if (alerts.Count() > creteria.pageSize)
       //                 {
       //                     alerts = searchalerts.Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
       //                 }
       //                 else
       //                 {
       //                     alerts = searchalerts.Take(creteria.pageSize).ToList();
       //                 }
       //             }
       //         }
       //         foreach (var item in alerts)
       //         {
       //             var alertDto = new AlertsDto();
       //             alertDto.WellName = item.WellName;
       //             alertDto.AlertStatus = item.AlertStatus;
       //             alertDto.AlertLevel = item.AlertLevel;
       //             alertDto.AlertDescription = item.AlertDescription;
       //             alertDto.Id= item.Id;
       //             alertDto.TimeandDate= item.TimeandDate;
       //             alertDto.AlertType=    item.AlertType;
       //             alertDto.UserId= item.UserId;
       //             alertssDto.Add(alertDto);
       //         }

       //     }
       //     return Ok(JsonConvert.SerializeObject(new { success = true, data = alertssDto, totalCount = Count }));

       // }


        [HttpPost("GetAlertsList")]
        public async Task<IActionResult> Get(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate)
        {
            Guard.Against.InvalidPageIndex(pageIndex);
            Guard.Against.InvalidPageSize(pageSize);
            int Count = 0;

            var alertInJson = UtilityService.Read<List<AlertsDto>>
                                              (JsonFiles.alerts).AsQueryable();
            var result = await _alertsService.GetAlerts(pageIndex, pageSize, searchString, sortExpression, startDate, endDate);

            var alertSonnoze = alertInJson.Where(a => a.SnoozeFlag == true);
            if(alertSonnoze.Count() > 0) {
                var snooze= alertInJson.Where(a => a.SnoozeFlag == false);
                Count = snooze.Count();
            }
            else
            {
                Count = alertInJson.Count();
            }
      
            if (result != null && result?.Count() > 0) return Ok(JsonConvert.SerializeObject(new { success = true, data = result, totalCount = Count }));
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
                                      (JsonFiles.alerts).AsQueryable();
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
