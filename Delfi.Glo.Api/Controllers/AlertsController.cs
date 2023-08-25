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
    public class AlertsController : ControllerBase
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
                if(startDate!=null && endDate!=null)
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
    }
}
