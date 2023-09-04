using Ardalis.GuardClauses;
using Delfi.Glo.Api.Exceptions;
using Delfi.Glo.Api.Extensions;
using Delfi.Glo.Common;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace Delfi.Glo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : ControllerBase
    {
        private readonly ILogger<WellController> _logger;
        private readonly ICrudService<WellDto> _wellService;
        private readonly IFilterService<WellDto,SearchCreteria> _filterService;


        public WellController(ILogger<WellController> logger, ICrudService<WellDto> wellService, IFilterService<WellDto, SearchCreteria> filterService)
        {
            _logger = logger;
            _wellService = wellService;
            _filterService = filterService;
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
            Tuple<bool, IEnumerable<WellDto>, int, int, int, int> values = await _filterService.GetListByFilter(creteria);
            return Ok(JsonConvert.SerializeObject(new { success = values.Item1, data = values.Item2, totalCount = values.Item3, totalWellPriorityHigh = values.Item4, totalWellPriorityMedium = values.Item5, totalWellPriorityLow = values.Item6 }));

        }
    }
}
