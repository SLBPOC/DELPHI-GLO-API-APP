using Delfi.Glo.Common;
using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class WellService : ICrudService<WellDto>, IFilterService<WellDto>
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
        public async Task<IEnumerable<WellDto>> GetFromJsonFile()
        {
            var wellsInJson = UtilityService.Read<List<WellDto>> (JsonFiles.Wells).AsQueryable();
            return wellsInJson;
        }
        public async Task<Tuple<bool, IEnumerable<WellDto>, int, int, int, int>> GetListByFilter(int page, int pageSize, string? searchString, string? ApprovalStatus, string? ApprovalMode, List<SortExpression> sortExpression)
        {
                var wellsDto = new List<WellDto>();
                int Count = 0;
                int WellPriorityHigh = 0;
                int WellPriorityMedium = 0;
                int WellPriorityLow = 0;

                var wells = UtilityService.Read<List<WellDto>>(JsonFiles.Wells).AsQueryable();
                Count = wells.Count();
               if (wells != null)
                {
                    WellPriorityHigh = wells.Where(a => a.WellPriority == "High").Count();
                    WellPriorityMedium = wells.Where(a => a.WellPriority == "Medium").Count();
                    WellPriorityLow = wells.Where(a => a.WellPriority == "Low").Count();
         
                if (searchString != null)
                {
                    string search = searchString.ToLower();
                    if (searchString.Length > 0)
                    {
                        var spec = new WellSpecification(search);
                        var wellsList = wells.Where(spec.ToExpression());

                        if (ApprovalStatus != null && ApprovalMode != null)
                        //    if (ApprovalStatus != null)
                        {
                            wellsList = wells.Where(c => c.ApprovalStatus.ToLower() == ApprovalStatus.ToLower() && c.ApprovalMode.ToLower() == ApprovalMode.ToLower());
                        }
                        else if (ApprovalStatus != null && ApprovalMode == null)
                        {
                            wellsList = wells.Where(c => c.ApprovalStatus.ToLower() == ApprovalStatus.ToLower());
                        }
                        else if (ApprovalStatus == null && ApprovalMode != null)
                        {
                            wellsList = wells.Where(c => c.ApprovalMode.ToLower() == ApprovalMode.ToLower());
                        }

                        wellsList = DynamicSort.ApplyDynamicSort(wellsList, sortExpression);
                        wellsDto = wellsList.Skip((page - 1) * pageSize).Take(pageSize).ToList();


                    }
                }
                else
                {
                    var wellsList = wells;
                    if (ApprovalStatus != null && ApprovalMode != null)
                    //    if (ApprovalStatus != null)
                    {
                        wellsList = wells.Where(c => c.ApprovalStatus.ToLower() == ApprovalStatus.ToLower() && c.ApprovalMode.ToLower() == ApprovalMode.ToLower());
                    }
                    else if (ApprovalStatus != null && ApprovalMode == null)
                    {
                        wellsList = wells.Where(c => c.ApprovalStatus.ToLower() == ApprovalStatus.ToLower()); 
                    }
                    else if (ApprovalStatus == null && ApprovalMode != null)
                    {
                        wellsList = wells.Where(c => c.ApprovalMode.ToLower() == ApprovalMode.ToLower());
                    }
                    wellsList = DynamicSort.ApplyDynamicSort(wellsList, sortExpression);
                    wellsDto = wellsList.Skip((page-1) * pageSize).Take(pageSize).ToList();

                }

            }
                return new Tuple<bool, IEnumerable<WellDto>, int, int, int, int>(true, wellsDto, Count, WellPriorityHigh, WellPriorityMedium, WellPriorityLow);
        }
       
     
        public async Task<IEnumerable<WellDto>> GetAllAsync()
        {
            var wells = await GetFromJsonFile();
            //var wells =  _dbUnit.wells.GetAll().ToList();
            var wellsDto = new List<WellDto>();
            if (wells == null) return null;
            foreach (var well in wells)
            {
                var wellDto = new WellDto();
                wellDto.Id = well.Id;
                wellDto.WellName = well.WellName;
                wellDto.WellPriority = well.WellPriority;
                wellDto.GLISetPoint = well.GLISetPoint;
                wellDto.QOil = well.QOil;
                wellDto.QLiq = well.QLiq;
                wellDto.Qg = well.Qg;
                wellDto.Qw = well.Qw;
                wellDto.Wc = well.Wc;
                wellDto.CompressorUpTime = well.CompressorUpTime;
                wellDto.ProductionUpTime = well.ProductionUpTime;
                wellDto.DeviceUpTime = well.DeviceUpTime;
                wellDto.LastCycleStatus = well.LastCycleStatus;
                wellDto.TimeStamp = well.TimeStamp;
                wellDto.CurrentGLISetpoint = well.CurrentGLISetpoint;
                wellDto.CurrentCycleStatus = well.CurrentCycleStatus;
                wellDto.ApprovalMode = well.ApprovalMode;
                wellDto.ApprovalStatus = well.ApprovalStatus;
                wellDto.UserId = well.UserId;
                wellDto.NoOfAlerts = well.NoOfAlerts;
                wellsDto.Add(wellDto);
            }
            return wellsDto;
        }
        public async Task<WellDto> GetAsync(int id)
        {
            var wells = await GetFromJsonFile();
            WellDto well = wells.FirstOrDefault(x => x.Id == id);
            if (well == null) return null;
            var wellDto = new WellDto();
            wellDto.Id = well.Id;
            wellDto.WellName = well.WellName;
            wellDto.WellPriority = well.WellPriority;
            wellDto.GLISetPoint = well.GLISetPoint;
            wellDto.QOil = well.QOil;
            wellDto.QLiq = well.QLiq;
            wellDto.Qg = well.Qg;
            wellDto.Qw = well.Qw;
            wellDto.Wc = well.Wc;
            wellDto.CompressorUpTime = well.CompressorUpTime;
            wellDto.ProductionUpTime = well.ProductionUpTime;
            wellDto.DeviceUpTime = well.DeviceUpTime;
            wellDto.LastCycleStatus = well.LastCycleStatus;
            wellDto.TimeStamp = well.TimeStamp;
            wellDto.CurrentGLISetpoint = well.CurrentGLISetpoint;
            wellDto.CurrentCycleStatus = well.CurrentCycleStatus;
            wellDto.ApprovalMode = well.ApprovalMode;
            wellDto.ApprovalStatus = well.ApprovalStatus;
            wellDto.UserId = well.UserId;
            wellDto.NoOfAlerts = well.NoOfAlerts;
            return wellDto;
        }
        public Task<WellDto> UpdateAsync(int id, WellDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WellDto>> GetAllListByJson()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WellDto>> GetWells()
        {
            var wellsInJson = UtilityService.Read<List<WellDto>>(JsonFiles.Wells).AsQueryable();
            List<WellDto> alertCustomList = wellsInJson.ToList();
            //var spec = new WellNameSpecification();
            //var obj = wellsInJson.Select(spec.ToExpression());
            //var items = alertCustomList.Select(m => new { m.Id, m.WellName });
            List<WellDto> Item = alertCustomList.Select(m => new WellDto
            {
                Id = m.Id,
                WellName = m.WellName
            }).Distinct().ToList();
            //var Item = alertCustomList.Select(m => new { m.Id, m.WellName }).ToList();
            //var spec = new CustomAlertSpecification();
            //var obj = eventInJson.FirstOrDefault(spec.ToExpression());
            return Item;
        }
    }
}
