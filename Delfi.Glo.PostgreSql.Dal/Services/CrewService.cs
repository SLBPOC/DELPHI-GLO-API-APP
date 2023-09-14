using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class CrewService : ICrudService<CrewDto>
    {
        private readonly DbUnitWork _dbUnit;

        public CrewService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }

        public async Task<CrewDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CrewDto>> GetAllAsync()
        {
            var crews = _dbUnit.crews.GetAll().ToList();
            var crewsDto = new List<CrewDto>();
            foreach (var crew in crews)
            {
                var crewDto = new CrewDto();
                crewDto.CrewName = crew.CrewName;
                crewDto.Id = crew.Id;
                crewsDto.Add(crewDto);
            }
            return crewsDto;
        }

        public async Task<bool> ExistsAsync(int id) => throw new NotImplementedException();

        public async Task<CrewDto> CreateAsync(CrewDto crew)
        {
            Crew _crew = new Crew();
            _crew.CrewName = crew.CrewName;
   
         //   _dbUnit.crews.Create(_crew);
            await _dbUnit.SaveChangesAsync();
            return crew;
        }

        public async Task<CrewDto> UpdateAsync(int id, CrewDto crew)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CrewDto>> GetFromJsonFile()
        {
            throw new NotImplementedException();
        }

        public Task<CrewDto> CreateAsyncAlertCustom(CrewDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsyncAlertCustom(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            throw new NotImplementedException();
        }

        public Task<CrewDto> GetAlertCustomByAlertId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CrewDto>> GetAllListByJson()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CrewDto>> GetWells()
        {
            throw new NotImplementedException();
        }
    }
}
