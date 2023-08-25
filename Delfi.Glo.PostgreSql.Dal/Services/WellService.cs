using Delfi.Glo.Common;
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
    public class WellService : ICrudService<WellDto>
    {
        private readonly DbUnitWork _dbUnit;

        public WellService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }

        public Task<WellDto> CreateAsync(WellDto item)
        {
            throw new NotImplementedException();
        }

        public Task<WellDto> CreateAsyncAlertCustom(WellDto item)
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

        public Task<WellDto> GetAlertCustomByAlertId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WellDto>> GetAllAsync()
        {
            var wells =  _dbUnit.wells.GetAll().ToList();
            var wellsDto = new List<WellDto>();
            if (wells == null) return null;
            foreach (var well in wells)
            {
                var wellDto = new WellDto();
                wellDto.Id = well.Id;
                wellDto.WellName = well.WellName;
              //  wellDto.PumpingStatus = well.PumpStatus;
                wellDto.PumpStatus = well.PumpStatus;
                //wellDto.Qi = well.Qi;
                //wellDto.Qw = well.Qw;
                wellDto.Wc = well.Wc;
                wellDto.GlInjectionSetPoint = well.GlInjectionSetPoint;
                wellDto.CompressorUpTime = well.CompressorUpTime;
                wellDto.DeviceUpTime = well.DeviceUpTime;
                wellDto.TimeStamp = well.TimeStamp;
                wellDto.GLISetPoint = well.GLISetPoint;
                wellDto.OLiq = well.OLiq;
                wellDto.QOil = well.QOil;
                wellDto.LastCycleStatus = well.LastCycleStatus;
                wellDto.CurrentGLISetpoint = well.CurrentGLISetpoint;
                wellDto.CycleStatus = well.CycleStatus;
                wellDto.ApprovalMode = well.ApprovalMode;
                wellDto.ApprovalStatus = well.ApprovalStatus;
                wellsDto.Add(wellDto);
            }
            return wellsDto;
        }
        public async Task<WellDto> GetAsync(int id)
        {
            Well well = _dbUnit.wells.FirstOrDefault(x => x.Id == id);
            if (well == null) return null;
            var wellDto = new WellDto();
            wellDto.Id = well.Id;
            wellDto.WellName = well.WellName;
           // wellDto.PumpingStatus = well.PumpStatus;
            wellDto.PumpStatus = well.PumpStatus;
            //wellDto.Qi = well.Qi;
            //wellDto.Qw = well.Qw;
            wellDto.Wc = well.Wc;
            wellDto.GlInjectionSetPoint = well.GlInjectionSetPoint;
            wellDto.CompressorUpTime = well.CompressorUpTime;
            wellDto.DeviceUpTime = well.DeviceUpTime;
            wellDto.TimeStamp = well.TimeStamp;
            wellDto.GLISetPoint = well.GLISetPoint;
            wellDto.OLiq = well.OLiq;
            wellDto.QOil = well.QOil;
            wellDto.LastCycleStatus = well.LastCycleStatus;
            wellDto.CurrentGLISetpoint = well.CurrentGLISetpoint;
            wellDto.CycleStatus = well.CycleStatus;
            wellDto.ApprovalMode = well.ApprovalMode;
            wellDto.ApprovalStatus = well.ApprovalStatus;
            return wellDto;
        }

        public Task<IEnumerable<WellDto>> GetFromJsonFile()
        {
            throw new NotImplementedException();
        }

        public Task<WellDto> UpdateAsync(int id, WellDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            throw new NotImplementedException();
        }
    }
}
