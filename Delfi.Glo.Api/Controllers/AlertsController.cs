using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Delfi.Glo.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : Controller
    {
        private readonly ILogger<AlertsController> _logger;
        private readonly ICrudService<AlertsDto> _alertsService;
        public AlertsController(ILogger<AlertsController> logger, ICrudService<AlertsDto> alertsService)
        {
            _logger = logger;
            _alertsService = alertsService;

        }
        [HttpGet()]
        public async Task<IEnumerable<AlertsDto>> Get()
        {

            return await _alertsService.GetAllAsync();
        }
        [HttpGet("Id")]
        public async Task<ActionResult<AlertsDto>> Get(int id)
        {


            return await _alertsService.GetAsync(id);
        }
        [HttpGet("GetAllAlertListByJson")]
        public async Task<IEnumerable<AlertsDto>> GetAllAlertListByJson()
        {
            var items = new List<AlertsDto>();
            using (StreamReader r = new StreamReader("JSON/Alert.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<AlertsDto>>(json);
            }
            return items;
        }
        [HttpPost("GetAlertsList")]
        public async Task<ActionResult> GetAlertsListByFilters(SearchCreteria creteria, DateTime? startDate, DateTime? endDate)
       {
            var alertssDto = new List<AlertsDto>();
            int Count = 0;

            var alerts = await GetAllAlertListByJson();


            if (alerts != null)
            {
                foreach (var alert in alerts.Where(a => a.SnoozeFlag == true))
                {
                    var snInterval = (alert.SnoozeInterval == null || alert.SnoozeInterval == "" ? "0" : alert.SnoozeInterval);
                    var snFlag = alert.SnoozeFlag;
                    var snDateTime = alert.SnoozeDateTime;
                    var s = Convert.ToDateTime(snDateTime).AddHours(Convert.ToInt32(snInterval));
                    if (s < DateTime.Now)
                        alert.SnoozeFlag = false;
                    else
                        alert.SnoozeFlag = true;
                }
                alerts = alerts.Where(a => a.SnoozeFlag == false);

                if (startDate!=null && endDate!=null)
                {
                    alerts = alerts.Where(c => c.TimeandDate >= startDate && c.TimeandDate <= endDate);
                }
      

        Count = alerts.Count();

                if (creteria != null)
                {
                    var searchalerts = alerts;
                    if (creteria.searchString.Length > 0 && alerts.Count()>0)
                    {
                        searchalerts = alerts.Where(a => a.WellName.Contains(creteria.searchString )||a.AlertStatus.Contains(creteria.searchString) || a.AlertType.Contains(creteria.searchString) || a.AlertLevel.Contains(creteria.searchString)|| a.AlertDescription.Contains(creteria.searchString)).ToList();
                        Count = searchalerts.Count();

                    }

                    if (creteria.Status.Length > 0)
                    {
                        searchalerts = searchalerts.Where(a => a.AlertStatus == creteria.Status).ToList();
                        alerts = searchalerts;
                    }

                    if (creteria.field != null && creteria.field != "" && creteria.field != null && creteria.dir != "")
                    {
                        //Sorting enabled
                        if (alerts.Count() > creteria.pageSize)
                        {
                            if (creteria.dir == "asc")
                            {
                                alerts = searchalerts.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                            }
                            else
                            {
                                alerts = searchalerts.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                            }

                        }
                        else
                        {
                            if (creteria.dir == "asc")
                            {
                                alerts = searchalerts.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
                            }
                            else
                            {
                                alerts = searchalerts.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
                            }
                        }
                    }
                    else
                    {
                        //Default , no sorting enabled
                        if (alerts.Count() > creteria.pageSize)
                        {
                            alerts = searchalerts.Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                        }
                        else
                        {
                            alerts = searchalerts.Take(creteria.pageSize).ToList();
                        }
                    }
                }
                foreach (var item in alerts)
                {
                    var alertDto = new AlertsDto();
                    alertDto.WellName = item.WellName;
                    alertDto.AlertStatus = item.AlertStatus;
                    alertDto.AlertLevel = item.AlertLevel;
                    alertDto.AlertDescription = item.AlertDescription;
                    alertDto.Id= item.Id;
                    alertDto.TimeandDate= item.TimeandDate;
                    alertDto.AlertType=    item.AlertType;
                    alertDto.UserId= item.UserId;
                    alertssDto.Add(alertDto);
                }

            }
            return Ok(JsonConvert.SerializeObject(new { success = true, data = alertssDto, totalCount = Count }));

        }
        [HttpPost("GetSnoozeByAlert")]
        public async Task<ActionResult> GetSnoozeByAlert(int alertId, string snoozeBy)
        {
            int Count = 0;
            int High = 0;
            int Medium = 0;
            int Low = 0;
            int Cleared = 0; 
            var filePath = @"D:\Delfi-glo\GLO-Back\GasLift\Delfi.Glo.Api\JSON\Alert.json";
            var alertsList = await GetAllAlertListByJson();
            alertsList.First(a => a.Id == alertId).SnoozeFlag = true;
            alertsList.First(a => a.Id == alertId).SnoozeDateTime = DateTime.Now.ToString();
            alertsList.First(a => a.Id == alertId).SnoozeInterval = snoozeBy;
            var jsonData = JsonConvert.SerializeObject(alertsList, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            if (alertsList != null)
            {
                Count = alertsList.Count();
                High = alertsList.Where(a => a.AlertLevel == "High").Count();
                Medium = alertsList.Where(a => a.AlertLevel == "Medium").Count();
                Low = alertsList.Where(a => a.AlertLevel == "Low").Count();
                Cleared = alertsList.Where(a => a.AlertLevel == "Cleared").Count();
            }
            return Json(new { success = true, data = alertsList, totalCount = Count, totalHigh = High, totalMedium = Medium, totalLow = Low, totalCleared = Cleared });
        }

        [HttpPost("ClearAlert")]
        public async Task<bool> SetClearAlert(int alertId, string comment)
        {
            var filePath = @"D:\Delfi-glo\GLO-Back\GasLift\Delfi.Glo.Api\JSON\Alert.json";
            var alertsList = await GetAllAlertListByJson();
            alertsList.First(a => a.Id == alertId).Comment = comment;
            alertsList.First(a => a.Id == alertId).AlertLevel = "Cleared";



            var jsonData = JsonConvert.SerializeObject(alertsList, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            return true;
        }
    }
}
