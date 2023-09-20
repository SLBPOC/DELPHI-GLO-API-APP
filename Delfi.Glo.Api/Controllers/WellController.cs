using Ardalis.GuardClauses;
using Delfi.Glo.Api.Exceptions;
using Delfi.Glo.Api.Extensions;
using Delfi.Glo.Common;
using Delfi.Glo.Common.Helpers;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Delfi.Glo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : ControllerBase
    {
        private readonly ILogger<WellController> _logger;
        private readonly ICrudService<WellDto> _wellService;
        private readonly IFilterService<WellDto> _filterService;
        private readonly IWellService<WellDetailsDto> _wellDetailsService;
        private readonly IWellDetailsInfoService<SwimLaneGraphDetails> _swimLaneService;
        public WellController(ILogger<WellController> logger, ICrudService<WellDto> wellService, IFilterService<WellDto> filterService,IWellService<WellDetailsDto> wellDetailsService,IWellDetailsInfoService<SwimLaneGraphDetails> swimLaneGraphDetails)
        {
            _logger = logger;
            _wellService = wellService;
            _filterService = filterService;
            _wellDetailsService= wellDetailsService;
            _swimLaneService = swimLaneGraphDetails;
        }


        [HttpGet("GetWellName")]
        public async Task<IEnumerable<WellDto>> GetWellName()
        {

            return await _wellService.GetWells();
        }

        [HttpGet()]
        public async Task<IEnumerable<WellDto>> Get()
        {

            return await _wellService.GetAllAsync();
        }

        [HttpGet("Id")]
        public async Task<ActionResult<WellDto>> Get(int id)
        {
            var result = await _wellDetailsService.GetWellDetailsInfoById(id);
            if (result != null) return Ok(JsonConvert.SerializeObject(new { success = true, data = result }));
            else return NotFound($"No Well found with id {id}");
        }

        [HttpGet("SwimLaneGraph")]
        public async Task<ActionResult<WellDto>> Get(int id, DateTime StartDate, DateTime EndDate)
        {
            var result = await _swimLaneService.GetSwimLaneDetailsByDate(id,StartDate,EndDate);
            if (result != null) return Ok(JsonConvert.SerializeObject(new { success = true, data = result }));
            else return NotFound($"No Well found with id {id}");
        }
        [HttpPost("GetWellList")]
        public async Task<ActionResult> GetWellList(int pageIndex, int pageSize, string? searchString, string? ApprovalStatus, string? ApprovalMode, List<SortExpression> sortExpression)
        {
            //List<SortExpression> sortExpressions1 = JsonConvert.DeserializeObject<List<SortExpression>>(sortExpression);
            Guard.Against.InvalidPageIndex(pageIndex);
            Guard.Against.InvalidPageSize(pageSize);
            Tuple<bool, IEnumerable<WellDto>, int, int, int, int> values = await _filterService.GetWellListByFilter(pageIndex, pageSize, searchString, ApprovalStatus, ApprovalMode, sortExpression);
            return Ok(JsonConvert.SerializeObject(new { success = values.Item1, data = values.Item2, totalCount = values.Item3, totalWellPriorityHigh = values.Item4, totalWellPriorityMedium = values.Item5, totalWellPriorityLow = values.Item6 }));
        }
    }
}
