using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using System.Collections.Immutable;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class WellGeneralInfoService : IGeneralInfoService<WellGeneralInfoDto>

    {
        private readonly DbUnitWork _dbUnit;

        public WellGeneralInfoService(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }

        //public async Task<IEnumerable<WellDto>> GetAllAsync()
        //{
        //    var eventInJson = UtilityService.Read<List<CustomAlertDto>>
        //                                            (JsonFiles.customAlert).ToList();

        //    List<CustomAlertDto> alertCustomList = eventInJson;

        //    var generalInfosDto = new List<WellDto>();


        //    foreach (var generalInfo in lstGeneralInfo)
        //    {
        //        var generalInfoDto = new WellGeneralInfoDto();
        //        generalInfoDto.Id = generalInfo.Id;
        //        generalInfoDto.Qo = generalInfo.Qo;
        //        generalInfoDto.Ql = generalInfo.Ql;
        //        generalInfoDto.Qw = generalInfo.Qw;
        //        generalInfoDto.Qg = generalInfo.Qg;
        //        generalInfoDto.Wc = generalInfo.Wc;
        //        generalInfoDto.GlInjectionSetPoint = generalInfo.GlInjectionSetPoint;
        //        generalInfoDto.CompressorUpTime = generalInfo.CompressorUpTime;
        //        generalInfoDto.DeviceUpTime = generalInfo.DeviceUpTime;
        //        generalInfoDto.ProcessorState = generalInfo.ProcessorState;
        //        generalInfoDto.ApprovalMode = generalInfo.ApprovalMode;
        //        generalInfoDto.WellViewComment1 = generalInfo.WellViewComment1;
        //        generalInfoDto.WellViewComment2 = generalInfo.WellViewComment2;
        //        generalInfoDto.WellViewComment3 = generalInfo.WellViewComment3;
        //        generalInfoDto.WellViewComment4 = generalInfo.WellViewComment4;
        //        generalInfosDto.Add(generalInfoDto);
        //    }


        //    return generalInfosDto;


        //}

        public async Task<WellGeneralInfoDto> GetAsync(int id)
        {
            var eventInJson = UtilityService.Read<List<WellGeneralInfoDto>>
                                                    (JsonFiles.Wells).AsQueryable();
            List<WellGeneralInfoDto> alertCustomList = eventInJson.ToList();
            var spec = new GeneralInfoSpecification(id);
            var obj = eventInJson.FirstOrDefault(spec.ToExpression());

            if (obj == null)
            {
                return null;
            }
            return obj;
        }

    }
}
