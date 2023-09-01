using Ardalis.GuardClauses;
using Delfi.Glo.Api.Exceptions;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Delfi.SRP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService<UniversitiesDto> universityService;

        public UniversityController(IUniversityService<UniversitiesDto> universityService)
        {
            this.universityService = universityService;
        }

        [HttpPost()]
        public async Task<IActionResult> Get(int pageIndex, int pageSize, string ?universityName, List<SortExpression> sortExpression)
        {
            Guard.Against.InvalidPageIndex(pageIndex);
            Guard.Against.InvalidPageSize(pageSize);
            var result =  await universityService.GetUniversities(pageIndex, pageSize, universityName,sortExpression);
      
            if (result != null && result?.Count() > 0) return Ok(result);
            else return NotFound($"No university found with name {universityName}");
        }
    }
}
