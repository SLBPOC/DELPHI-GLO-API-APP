using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using System.Collections.Immutable;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class WellGeneralInfoService : IGeneralInfoService<WellDto>

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

        public async Task<WellDto> GetAsync(int id)
        {
            var eventInJson = UtilityService.Read<List<WellDto>>
                                                    (JsonFiles.Wells).AsQueryable();
            List<WellDto> alertCustomList = eventInJson.ToList();
            var spec = new GeneralInfoSpecification(id);
            var obj = eventInJson.FirstOrDefault(spec.ToExpression());

            // var detail = new WellDto();

            //detail.Id = obj.Id;
            //detail.QOil = obj.QOil;
            //detail.QLiq = obj.QLiq;
            //detail.Qw = obj.Qw;
            //detail.Qg = obj.Qg;
            //detail.Wc = obj.Wc;
            //detail.GLISetPoint = obj.GLISetPoint;
            //detail.CompressorUpTime = obj.CompressorUpTime;
            //detail.DeviceUpTime = obj.DeviceUpTime;
            //detail.CurrentCycleStatus = obj.CurrentCycleStatus;
            //detail.ApprovalMode = obj.ApprovalMode;

            //return detail;
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        
    }
}
