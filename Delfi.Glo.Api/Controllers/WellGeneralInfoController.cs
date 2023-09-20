using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Delfi.Glo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WellGeneralInfoController : ControllerBase
    {
        private readonly ILogger<WellGeneralInfoController> _logger;
        private readonly IGeneralInfoService<WellGeneralInfoDto> _wellGeneralInfoService;

        public WellGeneralInfoController(ILogger<WellGeneralInfoController> logger, IGeneralInfoService<WellGeneralInfoDto> wellGeneralInfoService)
        {
            _logger = logger;
            _wellGeneralInfoService = wellGeneralInfoService;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<WellGeneralInfoDto>> Get(int Id)
        {
            return await _wellGeneralInfoService.GetWellGeneralInfoAsync(Id);
        }

        [HttpGet("GetSM38LfirstChartData")]
        public async Task<ActionResult<IEnumerable<SM38L_firstChartDto>>> Get()
        {
            var SM38L_firstChart = UtilityService.Read<List<SM38L_firstChartDto>>
                                                   (JsonFiles.SM38L_firstChartDto).AsQueryable();
            List<SM38L_firstChartDto> obj_SM38L_firstChartDto = SM38L_firstChart.ToList();

            return obj_SM38L_firstChartDto;
          
        }
    }
}
