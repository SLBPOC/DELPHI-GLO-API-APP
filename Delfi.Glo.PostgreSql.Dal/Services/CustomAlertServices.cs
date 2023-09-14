using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;
using Newtonsoft.Json;
using System.Linq;
using static Delfi.Glo.Entities.Dto.CustomAlertDto;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class CustomAlertServices : ICustomAlertService<CustomAlertDto>
    {
        private readonly DbUnitWork _dbUnit;

        public CustomAlertServices(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
       
        public async Task<IEnumerable<CustomAlertDto>> GetCustomAlert()
        { 
            var eventInJson = UtilityService.Read<List<CustomAlertDto>>
                                                    (JsonFiles.CustomAlerts).AsQueryable();   
            
            return eventInJson;
        }       
        public async Task<bool> CreateCustomAlert(CustomAlertDto alertCustom)
        {
            var eventInJson = UtilityService.Read<List<CustomAlertDto>>
                                                    (JsonFiles.CustomAlerts).ToList();
            
            List<CustomAlertDto> alertCustomList = eventInJson; 
            alertCustomList.Add(new CustomAlertDto()
            {
                Id = alertCustomList[alertCustomList.Count - 1].Id + 1,
                WellId = alertCustom.WellId,
                WellName = alertCustom.WellName,
                CustomAlertName = alertCustom.CustomAlertName,
                Category = alertCustom.Category,
                Priority = alertCustom.Priority,
                NotificationType = alertCustom.NotificationType,
                Operator = alertCustom.Operator,
                Value = alertCustom.Value,
                IsActive = alertCustom.IsActive,
                StartDate = alertCustom.StartDate,
                EndDate = alertCustom.EndDate
            });
            var filePath = JsonFiles.CustomAlerts;
            bool data = UtilityService.Write<CustomAlertDto>(alertCustomList, filePath);

           
            ///Add Custome alert in the event list 
            //var eventList = UtilityService.Read<List<EventDto>>
            //                     (JsonFiles.events).ToList();
            //int eventId = eventList.Max(u => u.Id);
            //int Event_ID = eventId + 1;

            //List<EventDto> event_List = eventList;
            //event_List.Add(new EventDto()
            //{
            //    Id = Event_ID,
            //    WellName = alertCustom.WellName,
            //    EventType = "",
            //    EventStatus = "",
            //    EventDescription = alertCustom.CustomAlertName +"-"+  + alertCustom.Value,
            //    UpdatedBy = "001",
            //    Priority = alertCustom.Priority,
            //});
            //var filePathEvent = JsonFiles.events;
            //UtilityService.Write<EventDto>(event_List, filePathEvent);

            return data;
        }

        public async Task<bool> DeleteCustomAlert(int id)
        {
            var eventInJson = UtilityService.Read<List<CustomAlertDto>>
                                                    (JsonFiles.CustomAlerts).AsQueryable();
            List<CustomAlertDto> alertCustomList = eventInJson.ToList();
            var spec = new CustomAlertSpecification(id);
            var obj = eventInJson.FirstOrDefault(spec.ToExpression());
            if (obj == null)
            {
                return false;
            }
            alertCustomList.Remove(obj);
            var filePath = JsonFiles.CustomAlerts;
            bool data = UtilityService.Write<CustomAlertDto>(alertCustomList, filePath);
            return data;
        }

        public async Task<bool> UpdateToggle(int id, bool check)
        {            
            var eventInJson = UtilityService.Read<List<CustomAlertDto>>
                                                   (JsonFiles.CustomAlerts).AsQueryable();
            List<CustomAlertDto> alertCustomList = eventInJson.ToList();
            var spec = new CustomAlertSpecification(id);
            var obj = eventInJson.FirstOrDefault(spec.ToExpression());
            if (obj == null)
            {
                return false;
            }
            obj.IsActive = check;
            var filePath = JsonFiles.CustomAlerts;
            bool data = UtilityService.Write<CustomAlertDto>(alertCustomList, filePath);            
            return true;
        }

        public async Task<CustomAlertDto> GetCustomAlertByAlertId(int id)
        {
            var eventInJson = UtilityService.Read<List<CustomAlertDto>>
                                                    (JsonFiles.CustomAlerts).AsQueryable();
            List<CustomAlertDto> alertCustomList = eventInJson.ToList();
            var spec = new CustomAlertSpecification(id);
            var obj = eventInJson.FirstOrDefault(spec.ToExpression());
            if (obj == null)
            {
                return null;
            }
            return obj;
        }
    }
}
