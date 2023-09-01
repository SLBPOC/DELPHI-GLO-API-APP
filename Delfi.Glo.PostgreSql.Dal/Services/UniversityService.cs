
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Delfi.Glo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Common.Constants;
using Microsoft.AspNetCore.Http;
using NinjaNye.SearchExtensions;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Common.Services.Interfaces;
using Delfi.Glo.PostgreSql.Dal.Specifications;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class UniversityService : IUniversityService<UniversitiesDto>
    {

        private readonly IHttpService<UniversitiesDto> httpService;

        public UniversityService(IHttpService<UniversitiesDto> httpService)
        {
            this.httpService = httpService;
        }

        public async Task<IEnumerable<UniversitiesDto>> GetUniversities(int page, int pageSize, string ?universityByName, List<SortExpression> sortExpression)
        {
           // var universitiesInJson = await httpService.HttpGetAll(string.Concat(UrlConstants.UNIVERSITYLIST));
            var universitiesInJson = UtilityService.Read<List<UniversitiesDto>>
                                                    (JsonFiles.UNIVERSITIES).AsQueryable();

            //universitiesInJson = universitiesInJson.Search(v => v.Name, v => v.Country, v => v.AlphaTwoCode
            //    ).Containing(universityByName);

            //var items = DynamicSort.ApplyDynamicSort(universitiesInJson, sortExpression);

            //var result = items.Skip(page * pageSize).Take(pageSize).ToList();

            var spec = new UniversityByNameSpecification(universityByName);

            var universities = universitiesInJson.Where(spec.ToExpression()).Skip(page * pageSize)
                                                                           .Take(pageSize)
                                                                           .OrderByDescending(un => un.Name)
                                                                           .ToList();
            return universities;
        }
    }
}
