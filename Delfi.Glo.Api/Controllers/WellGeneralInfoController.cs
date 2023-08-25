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
        private readonly ICrudService<WellGeneralInfoDto> _wellGeneralInfoService;

        public WellGeneralInfoController(ILogger<WellGeneralInfoController> logger, ICrudService<WellGeneralInfoDto> wellGeneralInfoService)
        {
            _logger = logger;
            _wellGeneralInfoService = wellGeneralInfoService;
        }

        [HttpGet()]
        public async Task<ActionResult<WellGeneralInfoDto>> Get(int id)
        {
            return await _wellGeneralInfoService.GetAsync(id);
        }
    }
}
