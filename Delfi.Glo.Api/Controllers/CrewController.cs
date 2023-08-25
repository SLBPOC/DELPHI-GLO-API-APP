using Microsoft.AspNetCore.Mvc;
using Delfi.Glo.Api.Authentication;
using Delfi.Glo.Common.Constants;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;

namespace Delfi.Glo.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    ////[ApiAuthorize]
    public class CrewController : ControllerBase
    {
        private readonly ILogger<CrewController> _logger;
        private readonly ICrudService<CrewDto> _crewService;

        public CrewController(ILogger<CrewController> logger, ICrudService<CrewDto> crewService)
        {
            _logger = logger;
            _crewService = crewService;
        }

        [HttpGet()]
        public async Task<IEnumerable<CrewDto>> Get()
        {
            return await _crewService.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CrewDto>> Create(CrewDto crew)
        {
            if (crew == null)
                return BadRequest();

            return await _crewService.CreateAsync(crew);
        }

        [HttpPut]
        public async Task<ActionResult<CrewDto>> Update(int id, CrewDto crew)
        {
            if (crew == null)
                return BadRequest();

            return await _crewService.UpdateAsync(id, crew);
        }

        [HttpDelete(RouteConstants.Id)]
        public async Task<ActionResult<bool>> Delete(int crewId)
        {
            return await _crewService.DeleteAsync(crewId);
        }

    }
}