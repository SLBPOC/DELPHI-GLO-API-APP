using Delfi.Glo.Common.Constants;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delfi.Glo.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiController]
    [Route("[controller]")]
    public class CustomAlertController : ControllerBase
    {
        private readonly ILogger<CustomAlertController> _logger;
        private readonly ICustomAlertService<CustomAlertDto> _customalertService;
        public CustomAlertController(ILogger<CustomAlertController> logger, ICustomAlertService<CustomAlertDto> customalertService)
        {
            _logger = logger;
            _customalertService = customalertService;

        }
        //[HttpGet()]
        //public async Task<IEnumerable<CustomAlertDto>> Get()
        //{

        //    return await _customalertService.GetAllAsync();
        //}

        [Route("[action]")]
        [HttpGet]
        public async Task<IEnumerable<CustomAlertDto>> GetFromJFile()
        {
            return await _customalertService.GetFromJsonFile();
        }

        [HttpPost("CreateAlertCustom")]
        public async Task<ActionResult> CreateAlertCustom(CustomAlertDto customAlert)
        {
            if (customAlert == null)
                return BadRequest();



            return Ok(await _customalertService.CreateAsyncAlertCustom(customAlert));
        }

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<ActionResult<CustomAlertDto>> Create([FromBody] CustomAlertDto customalert)
        //{
        //    if (customalert == null)
        //        return BadRequest();
        //    return await _customalertService.CreateAsync(customalert);

      
        //}
        //[HttpPut]
        //public async Task<ActionResult<CustomAlertDto>> Update(int id, CustomAlertDto customalert)
        //{
        //    if (customalert == null)
        //        return BadRequest();

        //    return await _customalertService.UpdateAsync(id, customalert);
        //}

        //[HttpDelete(RouteConstants.Id)]
        //public async Task<ActionResult<bool>> Delete(int Id)
        //{
        //    return await _customalertService.DeleteAsync(Id);
        //}

        [HttpDelete()]
        [Route("DeleteAlertCustom")]
        public async Task<ActionResult<bool>> DeleteAlertCustom(int Id)
        {
            return await _customalertService.DeleteAsyncAlertCustom(Id);
        }
        
        [HttpPut()]
        [Route("IsActiveAlertCustom")]
        public async Task<ActionResult<bool>> IsActiveAlertCustom(int id, bool IsActive)
        {
            return await _customalertService.UpdateAsyncAlertCustom(id, IsActive);
        }

        [HttpGet()]
        [Route("GetAlertCustom")]
        public async Task<ActionResult<CustomAlertDto>> GetAlertCustom(int Id)
        {
            return await _customalertService.GetAlertCustomByAlertId(Id);
        }
    }
}
