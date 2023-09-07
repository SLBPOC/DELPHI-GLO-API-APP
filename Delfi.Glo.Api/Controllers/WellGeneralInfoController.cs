using Delfi.Glo.Common.Constants;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Delfi.Glo.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WellGeneralInfoController : ControllerBase
    {
        private readonly ILogger<WellGeneralInfoController> _logger;
        private readonly IGeneralInfoService<WellDto> _wellGeneralInfoService;

        public WellGeneralInfoController(ILogger<WellGeneralInfoController> logger, IGeneralInfoService<WellDto> wellGeneralInfoService)
        {
            _logger = logger;
            _wellGeneralInfoService = wellGeneralInfoService;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<WellDto>> Get(int Id)
        {
            return await _wellGeneralInfoService.GetAsync(Id);
        }
    }
}
