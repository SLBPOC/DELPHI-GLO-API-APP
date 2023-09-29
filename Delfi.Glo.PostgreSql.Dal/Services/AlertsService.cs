using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using Newtonsoft.Json;
using System.Globalization;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class AlertsService : IAlertService<AlertsDto>
    {
        private readonly DbUnitWork _dbUnit;

        public AlertsService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
        public async Task<Tuple<IEnumerable<AlertsDto>, int>> GetAlerts(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate)
        {
            AddCustomAlertsInAlerts();

            var alertsDto = new List<AlertsDto>();
            int Count = 0;
            var alertInJson = UtilityService.Read<List<AlertsDto>>
                                              (JsonFiles.Alerts).AsQueryable();
            Count = alertInJson.Count();
            foreach (var alert in alertInJson.Where(a => a.SnoozeFlag == true))
            {
                var snInterval = (alert.SnoozeInterval == null || alert.SnoozeInterval == null ? 0 : alert.SnoozeInterval);
                var snFlag = alert.SnoozeFlag;
                var snDateTime = alert.SnoozeDateTime;
                var s = Convert.ToDateTime(snDateTime).AddHours(Convert.ToInt32(snInterval));
                if (s < DateTime.Now)
                    alert.SnoozeFlag = false;
                else
                    alert.SnoozeFlag = true;
            }
            alertInJson = alertInJson.Where(a => a.SnoozeFlag == false);

            ///get only last 7 days data
            DateTime lastDate = DateTime.Now;
            DateTime FirstDate = lastDate.AddDays(-7);
            alertInJson = alertInJson.Where(r => r.TimeandDate >= FirstDate && r.TimeandDate <= lastDate);


            if (searchString != null)
            {
                var Search = searchString.ToLower();
                var spec = new AlertsSpecification(Search);

                var alerts = alertInJson.Where(spec.ToExpression());

                if (startDate != null && endDate != null)
                {
                    alerts = alerts.Where(c => c.TimeandDate.Value.Year >= startDate.Value.Year
                                              && c.TimeandDate.Value.Month >= startDate.Value.Month
                                              && c.TimeandDate.Value.Day >= startDate.Value.Day

                    && c.TimeandDate.Value.Year <= endDate.Value.Year
                                              && c.TimeandDate.Value.Month <= endDate.Value.Month
                                              && c.TimeandDate.Value.Day <= endDate.Value.Day);

                }

                alerts = DynamicSort.ApplyDynamicSort(alerts, sortExpression);
                alertsDto = alerts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                Count = alertsDto.Count();

            }
            else
            {

                var alerts = alertInJson;
                if (startDate != null && endDate != null)
                {
                    //alerts = alerts.Where(c => c.TimeandDate >= startDate && c.TimeandDate <= endDate);
                    alerts = alerts.Where(c => c.TimeandDate.Value.Year >= startDate.Value.Year
                                               && c.TimeandDate.Value.Month >= startDate.Value.Month
                                               && c.TimeandDate.Value.Day >= startDate.Value.Day

                     && c.TimeandDate.Value.Year <= endDate.Value.Year
                                               && c.TimeandDate.Value.Month <= endDate.Value.Month
                                               && c.TimeandDate.Value.Day <= endDate.Value.Day);
                }
               
                alerts = DynamicSort.ApplyDynamicSort(alerts, sortExpression);
                Count = alerts.Count();
                alertsDto = alerts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            }
            return new Tuple<IEnumerable<AlertsDto>, int>(alertsDto, Count);
        }
        public async Task<IEnumerable<AlertsDto>> GetSnoozeByAlert(int alertId, int snoozeBy)
        {
            int Count = 0;
            int High = 0;
            int Medium = 0;
            int Low = 0;
            int Cleared = 0;
        
            string fileName = JsonFiles.Alerts;
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] fullFilePath = Directory.GetFiles(currentDirectory, fileName, SearchOption.AllDirectories);

            var alertsList = UtilityService.Read<List<AlertsDto>>
                                         (JsonFiles.Alerts).AsQueryable();
            alertsList.First(a => a.Id == alertId).SnoozeFlag = true;
            alertsList.First(a => a.Id == alertId).SnoozeDateTime = DateTime.Now.ToString();
            alertsList.First(a => a.Id == alertId).SnoozeInterval = snoozeBy;
            alertsList.First(a => a.Id == alertId).Comment = "";
            var jsonData = JsonConvert.SerializeObject(alertsList, Formatting.Indented);
            System.IO.File.WriteAllText(fullFilePath[0], jsonData);
            if (alertsList != null)
            {
                Count = alertsList.Count();
                High = alertsList.Where(a => a.AlertLevel == "High").Count();
                Medium = alertsList.Where(a => a.AlertLevel == "Medium").Count();
                Low = alertsList.Where(a => a.AlertLevel == "Low").Count();
                Cleared = alertsList.Where(a => a.AlertLevel == "Cleared").Count();
              
            }
            return alertsList;

        }
        public async Task<bool> SetClearAlert(int alertId, string comment)
        {
            string fileName = JsonFiles.Alerts;
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] fullFilePath = Directory.GetFiles(currentDirectory, fileName, SearchOption.AllDirectories);

            var alertsList = UtilityService.Read<List<AlertsDto>>
                                          (JsonFiles.Alerts).AsQueryable();
            alertsList.First(a => a.Id == alertId).Comment = comment;
            alertsList.First(a => a.Id == alertId).AlertLevel = "Cleared";
            alertsList.First(a => a.Id == alertId).AlertStatus = "Cleared";
            alertsList.First(a => a.Id == alertId).SnoozeFlag = false;

            alertsList.First(a => a.Id == alertId).SnoozeDateTime = "";
            alertsList.First(a => a.Id == alertId).SnoozeInterval = 0;
          
            var jsonData = JsonConvert.SerializeObject(alertsList, Formatting.Indented);
            System.IO.File.WriteAllText(fullFilePath[0], jsonData);


            //Add clear alert in the event list 
            var alerts = alertsList.First(a => a.Id == alertId);
            var eventList = UtilityService.Read<List<EventDto>>
                                 (JsonFiles.Events).ToList();
            int eventId = eventList.Max(u => u.Id);
            int Event_ID = eventId + 1;
            var alertDescription = alertsList.First(a => a.Id == alertId).AlertDescription;
            List<EventDto> event_List = eventList;
            event_List.Add(new EventDto()
            {
            Id = Event_ID,
            WellId = alerts.WellId,
            WellName = alerts.WellName,
            EventType = "Alert",
            EventStatus = "Cleared",
            EventDescription = alertDescription +" - "+comment,
            CreationDateTime = DateTime.Now,
            Priority = "High",
            UpdatedBy = "001",
            });
            var filePath = JsonFiles.Events;
            bool data = UtilityService.Write<EventDto>(event_List, filePath);

            return true;
        }
        private static void AddCustomAlertsInAlerts()
        {
            string fileName = JsonFiles.CustomAlerts;
            ///Get the custom alerts to show in alert screen with date condition
            var CustomAlert_json = UtilityService.Read<List<CustomAlertDto>>(JsonFiles.CustomAlerts).AsQueryable();
            var CustomeAlerts = CustomAlert_json.Where(x => DateTime.Parse(x.StartDate, null, DateTimeStyles.RoundtripKind) <= DateTime.Now && DateTime.Now <= DateTime.Parse(x.EndDate, null, DateTimeStyles.RoundtripKind) && x.IsShownInAlerts == false).ToList();
            var CustomeAlert_1 = CustomeAlerts.ToList();
            ///Check Category conditions on custom alert
            var Well_json = UtilityService.Read<List<WellDto>>(JsonFiles.Wells).AsQueryable();
            foreach (var ca in CustomeAlert_1)
            {
                var isFulfillAllConditions = true;
                if (ca.Category == "GLIR")
                {
                    if (ca.Operator == "=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.GLIR == ca.Value);
                    }
                    else if (ca.Operator == "<>")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.GLIR != ca.Value);
                    }
                    else if (ca.Operator == ">")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.GLIR > ca.Value);
                    }
                    else if (ca.Operator == "<")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.GLIR < ca.Value);
                    }
                    else if (ca.Operator == ">=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.GLIR >= ca.Value);
                    }
                    else if (ca.Operator == "<=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.GLIR <= ca.Value);
                    }
                }
                else if (ca.Category == "DP")
                {
                    if (ca.Operator == "=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.DP == ca.Value);
                    }
                    else if (ca.Operator == "<>")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.DP != ca.Value);
                    }
                    else if (ca.Operator == ">")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.DP > ca.Value);
                    }
                    else if (ca.Operator == "<")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.DP < ca.Value);
                    }
                    else if (ca.Operator == ">=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.DP >= ca.Value);
                    }
                    else if (ca.Operator == "<=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.DP <= ca.Value);
                    }
                }
                else if (ca.Category == "THP")
                {
                    if (ca.Operator == "=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.THP == ca.Value);
                    }
                    else if (ca.Operator == "<>")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.THP != ca.Value);
                    }
                    else if (ca.Operator == ">")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.THP > ca.Value);
                    }
                    else if (ca.Operator == "<")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.THP < ca.Value);
                    }
                    else if (ca.Operator == ">=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.THP >= ca.Value);
                    }
                    else if (ca.Operator == "<=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.THP <= ca.Value);
                    }
                }
                else if (ca.Category == "FLP")
                {
                    if (ca.Operator == "=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.FLP == ca.Value);
                    }
                    else if (ca.Operator == "<>")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.FLP != ca.Value);
                    }
                    else if (ca.Operator == ">")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.FLP > ca.Value);
                    }
                    else if (ca.Operator == "<")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.FLP < ca.Value);
                    }
                    else if (ca.Operator == ">=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.FLP >= ca.Value);
                    }
                    else if (ca.Operator == "<=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.FLP <= ca.Value);
                    }
                }
                else if (ca.Category == "CHP")
                {
                    if (ca.Operator == "=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.CHP == ca.Value);
                    }
                    else if (ca.Operator == "<>")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.CHP != ca.Value);
                    }
                    else if (ca.Operator == ">")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.CHP > ca.Value);
                    }
                    else if (ca.Operator == "<")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.CHP < ca.Value);
                    }
                    else if (ca.Operator == ">=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.CHP >= ca.Value);
                    }
                    else if (ca.Operator == "<=")
                    {
                        isFulfillAllConditions = Well_json.Any(x => x.Id == ca.WellId && x.CHP <= ca.Value);
                    }
                }

                if (!isFulfillAllConditions)
                {
                    CustomeAlerts.Remove(ca);
                }
            }


            ///Add Custom alert in the Alert list 
            var AlertList = UtilityService.Read<List<AlertsDto>>(JsonFiles.Alerts).ToList();
            int AlertId = AlertList.Max(u => u.Id);


            List<AlertsDto> alert_List = AlertList;
            foreach (var c in CustomeAlerts)
            {

                AlertId = AlertId + 1;
                alert_List.Add(new AlertsDto()
                {
                    Id = AlertId,
                    WellId = c.WellId,
                    WellName = c.WellName,
                    AlertLevel = c.Priority,
                    TimeandDate = DateTime.Now,
                    AlertDescription = "Custom Alert " + c.CustomAlertName,
                    AlertType = "Custom",
                    AlertStatus = "Created",
                    SnoozeFlag = false,
                    SnoozeDateTime = "",
                    SnoozeInterval = 0,
                    Comment = "Custom Alert " + c.CustomAlertName,
                    UserId = "U001",

                });
                CustomAlert_json.First(a => a.Id == c.Id).IsShownInAlerts = true;
                var jsonData = JsonConvert.SerializeObject(CustomAlert_json, Formatting.Indented);
                System.IO.File.WriteAllText(fileName, jsonData);
            }
            var filePathAlert = JsonFiles.Alerts;
            UtilityService.Write<AlertsDto>(alert_List, filePathAlert);
        }

    }
}
