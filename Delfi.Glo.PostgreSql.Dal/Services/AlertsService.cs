using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Migrations;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Hosting.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class AlertsService : IAlertService<AlertsDto>
    {
        private readonly DbUnitWork _dbUnit;

        public AlertsService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
        //public Task<AlertsDto> CreateAsync(AlertsDto item)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<AlertsDto> CreateAsyncAlertCustom(AlertsDto item)
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

        //public Task<bool> ExistsAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<AlertsDto> GetAlertCustomByAlertId(int id)
        //{
        //    throw new NotImplementedException();
        //}
 

        public async Task<IEnumerable<AlertsDto>> GetAlerts(int pageIndex, int pageSize, string? searchString, List<SortExpression> sortExpression, DateTime? startDate, DateTime? endDate)
        {
            var alertInJson = UtilityService.Read<List<AlertsDto>>
                                              (JsonFiles.alerts).AsQueryable();

            if (searchString != null)
            {
                var spec = new AlertsSpecification(searchString);

                var alerts = alertInJson.Where(spec.ToExpression());

                if (startDate != null && endDate != null)
                {
                    alerts = alerts.Where(c => c.TimeandDate >= startDate && c.TimeandDate <= endDate);
                }

                alerts = DynamicSort.ApplyDynamicSort(alerts, sortExpression);
                var result = alerts.Skip(0).Take(pageSize).ToList();

                return result;
            }
            else
            {


                var alerts = DynamicSort.ApplyDynamicSort(alertInJson, sortExpression);
                if (startDate != null && endDate != null)
                {
                    alerts = alerts.Where(c => c.TimeandDate >= startDate && c.TimeandDate <= endDate);
                }
                var result = alerts.Skip(pageIndex-1 * pageSize).Take(pageSize).ToList();

                return result;
            }
        }

        public async Task<IEnumerable<AlertsDto>> GetSnoozeByAlert(int alertId, string snoozeBy)
        {
            int Count = 0;
            int High = 0;
            int Medium = 0;
            int Low = 0;
            int Cleared = 0;
        
            string fileName = @"Alert.json";
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] fullFilePath = Directory.GetFiles(currentDirectory, fileName, SearchOption.AllDirectories);

            var alertsList = UtilityService.Read<List<AlertsDto>>
                                         (JsonFiles.alerts).AsQueryable();
            alertsList.First(a => a.Id == alertId).SnoozeFlag = true;
            alertsList.First(a => a.Id == alertId).SnoozeDateTime = DateTime.Now.ToString();
            alertsList.First(a => a.Id == alertId).SnoozeInterval = snoozeBy;
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
  
            string fileName = @"Alert.json";
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] fullFilePath = Directory.GetFiles(currentDirectory, fileName, SearchOption.AllDirectories);

            var alertsList = UtilityService.Read<List<AlertsDto>>
                                          (JsonFiles.alerts).AsQueryable();
            alertsList.First(a => a.Id == alertId).Comment = comment;
            alertsList.First(a => a.Id == alertId).AlertLevel = "Cleared";

            var jsonData = JsonConvert.SerializeObject(alertsList, Formatting.Indented);
            System.IO.File.WriteAllText(fullFilePath[0], jsonData);
            return true;
        }


        //        public async Task<IEnumerable<AlertsDto>> GetAllAsync()
        //        {
        //            var alerts = _dbUnit.alertss.GetAll().ToList();
        //            var alertsDto = new List<AlertsDto>();
        //            foreach (var item in alerts)
        //            {
        //                var alertDto = new AlertsDto();
        //                alertDto.Id = item.Id;
        //                alertDto.WellName = item.WellName;
        //                alertDto.AlertLevel = item.AlertLevel;
        //                alertDto.AlertStatus = item.AlertStatus;
        //                alertDto.AlertType = item.AlertType;
        //                alertDto.AlertDescription = item.AlertDescription;
        //                alertDto.TimeandDate = item.TimeandDate;

        //                alertsDto.Add(alertDto);
        //            }
        //            return alertsDto;
        //}

        //        public Task<IEnumerable<AlertsDto>> GetAllListByJson()
        //        {
        //            throw new NotImplementedException();
        //        }

        //        //public async Task<AlertsDto> GetAsync(int id)
        //        //{
        //        //    Alerts alert = _dbUnit.alertss.FirstOrDefault(x => x.Id == id);
        //        //    if (alert == null) return null;
        //        //    var alertDto = new AlertsDto();
        //        //    alertDto.Id = alert.Id;
        //        //    alertDto.WellName = alert.WellName;
        //        //    alertDto.AlertLevel = alert.AlertLevel;
        //        //    alertDto.AlertStatus = alert.AlertStatus;
        //        //    alertDto.AlertDescription = alert.AlertDescription;
        //        //    alertDto.TimeandDate= alert.TimeandDate;
        //        //    return alertDto;
        //        //}

        //        public Task<IEnumerable<AlertsDto>> GetFromJsonFile()
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public Task<AlertsDto> UpdateAsync(int id, AlertsDto item)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        //        {
        //            throw new NotImplementedException();
        //        }


    }
}
