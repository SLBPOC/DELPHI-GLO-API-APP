using Delfi.Glo.Common;
using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class WellService : ICrudService<WellDto>, IFilterService<WellDto,SearchCreteria>
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
            var wellsInJson = UtilityService.Read<List<WellDto>>
                                                   (JsonFileConstants.Wells).AsQueryable();
            return wellsInJson;
        }
        public async Task<Tuple<bool, IEnumerable<WellDto>, int, int, int, int>> GetListByFilter(SearchCreteria creteria)
        {
            var wellsDto = new List<WellDto>();
            int Count = 0;
            int WellPriorityHigh = 0;
            int WellPriorityMedium = 0;
            int WellPriorityLow = 0;

            var wells = await GetFromJsonFile();

            if (wells != null)
            {
                WellPriorityHigh = wells.Where(a => a.WellPriority == "High").Count();
                WellPriorityMedium = wells.Where(a => a.WellPriority == "Medium").Count();
                WellPriorityLow = wells.Where(a => a.WellPriority == "Low").Count();
                Count = wells.Count();

                if (creteria != null)
                {
                    var searchwells = wells;
                    string search = creteria.searchString.ToLower();
                    if (creteria.searchString.Length > 0)
                    {
                        searchwells = wells.Where(a => a.WellName.ToLower().Contains(search)
                        || a.WellPriority.ToLower().Contains(search)
                        || a.CompressorUpTime.ToString().Contains(search)
                        || a.ProductionUpTime.ToString().Contains(search)
                        || a.DeviceUpTime.ToString().Contains(search)
                        || a.GLISetPoint.ToString().Contains(search)
                        || a.QLiq.ToString().Contains(search)
                        || a.QOil.ToString().Contains(search)
                        || a.Qg.ToString().Contains(search)
                        || a.Qw.ToString().Contains(search)
                        || a.Wc.ToString().Contains(search)
                        || a.CurrentGLISetpoint.ToString().Contains(search)
                        || a.CurrentCycleStatus.ToLower().Contains(search)
                        || a.ApprovalMode.ToLower().Contains(search)
                        || a.ApprovalStatus.ToLower().Contains(search)

                         ).ToList();

                        Count = searchwells.Count();

                    }

                    if (creteria.Status.Length > 0)
                    {
                        searchwells = searchwells.Where(a => a.WellPriority == creteria.Status).ToList();
                        wells = searchwells;
                    }

                    if (creteria.field != null && creteria.field != "" && creteria.field != null && creteria.dir != "")
                    {
                        //Sorting enabled
                        if (wells.Count() > creteria.pageSize)
                        {
                            if (creteria.dir == "asc")
                            {
                                wells = searchwells.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                            }
                            else
                            {
                                wells = searchwells.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                            }

                        }
                        else
                        {
                            if (creteria.dir == "asc")
                            {
                                wells = searchwells.OrderBy(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
                            }
                            else
                            {
                                wells = searchwells.OrderByDescending(s => s.GetType().GetProperty(creteria.field).GetValue(s, null)).Take(creteria.pageSize).ToList();
                            }
                        }
                    }
                    else
                    {
                        //Default , no sorting enabled
                        if (wells.Count() > creteria.pageSize)
                        {
                            wells = searchwells.Skip(creteria.pageSize * (creteria.PageNumber - 1)).Take(creteria.pageSize).ToList();
                        }
                        else
                        {
                            wells = searchwells.Take(creteria.pageSize).ToList();
                        }
                    }
                }

                foreach (var well in wells)
                {
                    var wellDto = new WellDto();
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
    }
}
