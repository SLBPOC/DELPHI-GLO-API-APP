using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class AlertsService : ICrudService<AlertsDto>
    {
        private readonly DbUnitWork _dbUnit;

        public AlertsService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
        public Task<AlertsDto> CreateAsync(AlertsDto item)
        {
            throw new NotImplementedException();
        }

        public Task<AlertsDto> CreateAsyncAlertCustom(AlertsDto item)
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

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AlertsDto> GetAlertCustomByAlertId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AlertsDto>> GetAllAsync()
        {
            var alerts = _dbUnit.alertss.GetAll().ToList();
            var alertsDto = new List<AlertsDto>();
            foreach (var item in alerts)
            {
                var alertDto = new AlertsDto();
                alertDto.Id = item.Id;
                alertDto.WellName = item.WellName;
                alertDto.AlertLevel = item.AlertLevel;
                alertDto.AlertStatus = item.AlertStatus;
                alertDto.AlertType = item.AlertType;
                alertDto.AlertDescription = item.AlertDescription;
                alertDto.TimeandDate = item.TimeandDate;

                alertsDto.Add(alertDto);
            }
            return alertsDto;
}

        public Task<IEnumerable<AlertsDto>> GetAllListByJson()
        {
            throw new NotImplementedException();
        }

        public async Task<AlertsDto> GetAsync(int id)
        {
            Alerts alert = _dbUnit.alertss.FirstOrDefault(x => x.Id == id);
            if (alert == null) return null;
            var alertDto = new AlertsDto();
            alertDto.Id = alert.Id;
            alertDto.WellName = alert.WellName;
            alertDto.AlertLevel = alert.AlertLevel;
            alertDto.AlertStatus = alert.AlertStatus;
            alertDto.AlertDescription = alert.AlertDescription;
            alertDto.TimeandDate= alert.TimeandDate;
            return alertDto;
        }

        public Task<IEnumerable<AlertsDto>> GetFromJsonFile()
        {
            throw new NotImplementedException();
        }

        public Task<AlertsDto> UpdateAsync(int id, AlertsDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            throw new NotImplementedException();
        }


    }
}
