using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using System.Collections.Immutable;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class WellGeneralInfoService : ICrudService<WellGeneralInfoDto>

    {
        private readonly DbUnitWork _dbUnit;

        public WellGeneralInfoService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }

        public Task<WellGeneralInfoDto> CreateAsync(WellGeneralInfoDto item)
        {
            throw new NotImplementedException();
        }

        public Task<WellGeneralInfoDto> CreateAsyncAlertCustom(WellGeneralInfoDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsyncAlertCustom(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WellGeneralInfoDto> GetAlertCustomByAlertId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WellGeneralInfoDto>> GetAllAsync()
        {
            var lstGeneralInfo = _dbUnit.generalInfo.GetAll().ToList();

            var generalInfosDto = new List<WellGeneralInfoDto>();


            foreach (var generalInfo in lstGeneralInfo)
            {
                var generalInfoDto = new WellGeneralInfoDto();
                generalInfoDto.Id = generalInfo.Id;
                generalInfoDto.Qo = generalInfo.Qo;
                generalInfoDto.Ql = generalInfo.Ql;
                generalInfoDto.Qw = generalInfo.Qw;
                generalInfoDto.Qg = generalInfo.Qg;
                generalInfoDto.Wc = generalInfo.Wc;
                generalInfoDto.GlInjectionSetPoint = generalInfo.GlInjectionSetPoint;
                generalInfoDto.CompressorUpTime = generalInfo.CompressorUpTime;
                generalInfoDto.DeviceUpTime = generalInfo.DeviceUpTime;
                generalInfoDto.ProcessorState = generalInfo.ProcessorState;
                generalInfoDto.ApprovalMode = generalInfo.ApprovalMode;
                generalInfoDto.WellViewComment1 = generalInfo.WellViewComment1;
                generalInfoDto.WellViewComment2 = generalInfo.WellViewComment2;
                generalInfoDto.WellViewComment3 = generalInfo.WellViewComment3;
                generalInfoDto.WellViewComment4 = generalInfo.WellViewComment4;
                generalInfosDto.Add(generalInfoDto);
            }


            return generalInfosDto;


        }

        public async Task<WellGeneralInfoDto> GetAsync(int id)
        {
            var obj = _dbUnit.generalInfo.FirstOrDefault(x => x.Id == id);

            var detail = new WellGeneralInfoDto();

            detail.Id = obj.Id;
            detail.Qo = obj.Qo;
            detail.Ql = obj.Ql;
            detail.Qw = obj.Qw;
            detail.Qg = obj.Qg;
            detail.Wc = obj.Wc;
            detail.GlInjectionSetPoint = obj.GlInjectionSetPoint;
            detail.CompressorUpTime = obj.CompressorUpTime;
            detail.DeviceUpTime = obj.DeviceUpTime;
            detail.ProcessorState = obj.ProcessorState;
            detail.ApprovalMode = obj.ApprovalMode;
            detail.WellViewComment1 = obj.WellViewComment1;
            detail.WellViewComment2 = obj.WellViewComment2;
            detail.WellViewComment3 = obj.WellViewComment3;
            detail.WellViewComment4 = obj.WellViewComment4;

            return detail;
        }

        public Task<IEnumerable<WellGeneralInfoDto>> GetFromJsonFile()
        {
            throw new NotImplementedException();
        }

        public Task<WellGeneralInfoDto> UpdateAsync(int id, WellGeneralInfoDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            throw new NotImplementedException();
        }
    }
}
