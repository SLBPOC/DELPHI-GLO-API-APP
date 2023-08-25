using Delfi.Glo.Common;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Delfi.Glo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : ControllerBase
    {
        private readonly ILogger<WellController> _logger;
        private readonly ICrudService<WellDto> _wellService;
 

        public WellController(ILogger<WellController> logger, ICrudService<WellDto> wellService)
        {
            _logger = logger;
            _wellService = wellService;

        }


        [HttpGet()]
        public async Task<IEnumerable<WellDto>> Get()
        {

            return await _wellService.GetAllAsync();
        }

        [HttpGet("Id")]
        public async Task<ActionResult<WellDto>> Get(int id)
        {
    

            return await _wellService.GetAsync(id);
        }

        [HttpPost("GetWellList")]

        public async Task<ActionResult> GetWellListByFilters(SearchCreteria creteria)
        {
            var wellsDto = new List<WellDto>();
            int Count = 0;
            int OverPumping = 0;
            int OptimalPumping = 0;
            int UnderPumping = 0;

            var wells = await _wellService.GetAllAsync();
         
            if (wells != null)
            {
                OverPumping = wells.Where(a => a.PumpStatus == "Over Pumping").Count();
                OptimalPumping = wells.Where(a => a.PumpStatus == "Optimum Pumping").Count();
                UnderPumping = wells.Where(a => a.PumpStatus == "Under Pumping").Count();
                Count = wells.Count();

                if (creteria != null)
                {
                    var searchwells = wells;
                    if (creteria.searchString.Length > 0)
                    {
                        searchwells = wells.Where(a => a.WellName.Contains(creteria.searchString)).ToList();
                        Count = searchwells.Count();

                    }

                    if (creteria.Status.Length > 0)
                    {
                        searchwells = searchwells.Where(a => a.PumpStatus == creteria.Status).ToList();
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
                  //  wellDto.PumpingStatus = well.PumpStatus;
                    wellDto.PumpStatus = well.PumpStatus;
                  //  wellDto.Qi = well.Qi;
                   // wellDto.Qw = well.Qw;
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
                    wellDto.UserId = well.UserId;
                    wellsDto.Add(wellDto);
                }
            }
            return Ok(JsonConvert.SerializeObject(new { success = true, data = wellsDto, totalCount = Count, totalOverpumping = OverPumping, totalOptimalPumping = OptimalPumping, totalUnderpumping = UnderPumping }));

        }


    }
}
