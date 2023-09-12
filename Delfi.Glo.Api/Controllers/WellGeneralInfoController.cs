using Delfi.Glo.Common.Constants;
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
            return await _wellGeneralInfoService.GetAsync(Id);
        }
    }
}
