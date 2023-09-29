using Delfi.Glo.Common.Constants;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delfi.Glo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomAlertController : ControllerBase
    {
        private readonly ILogger<CustomAlertController> _logger;
        private readonly ICustomAlertService<CustomAlertDto> _customalertService;
        public CustomAlertController(ILogger<CustomAlertController> logger, ICustomAlertService<CustomAlertDto> customalertService)
        {
            _logger = logger;
            _customalertService = customalertService;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<CustomAlertDto>> Get()
        {
            return await _customalertService.GetCustomAlert();
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Post(CustomAlertDto customAlert)
        {
            if (customAlert == null)
                return BadRequest();

            return Ok(await _customalertService.CreateCustomAlert(customAlert));
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            return await _customalertService.DeleteCustomAlert(Id);
        }
        
        [HttpPut("IsActive")]
        public async Task<ActionResult<bool>> Put(int id, bool IsActive)
        {
            return await _customalertService.UpdateToggle(id, IsActive);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<CustomAlertDto>> Get(int Id)
        {
            return await _customalertService.GetCustomAlertByAlertId(Id);
        }
    }
}
