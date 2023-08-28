using Delfi.Glo.Entities.Db;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delfi.Glo.Entities;
using Newtonsoft.Json;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class CustomAlertServices : ICrudService<CustomAlertDto>
    {
        private readonly DbUnitWork _dbUnit;

        public CustomAlertServices(DbUnitWork dbUnit)
        {
            _dbUnit = dbUnit;
        }
        public async Task<CustomAlertDto> CreateAsync(CustomAlertDto item)
        {
           CustomAlert _customAlert = new CustomAlert();
            _customAlert.WellName=item.WellName;
           // _customAlert.Id=item.Id;
           // _customAlert.WellId = item.WellId;
            _customAlert.Category=item.Category;
            _customAlert.CustomAlertName=item.CustomAlertName;
            _customAlert.IsActive=item.IsActive;
            _customAlert.NotificationType=item.NotificationType;
            _customAlert.Value=item.Value;
            _customAlert.Priority=item.Priority;
            _customAlert.Operator=item.Operator;
            _customAlert.IsActive = item.IsActive;
            _customAlert.CreatedBy = "System";
            _customAlert.UpdatedBy   = "System";
            _customAlert.UserId = "System";
            _customAlert.EntityType = "CustomAlerts";
            _customAlert.Timestamp = DateTime.Now.ToUniversalTime();
            _dbUnit.customalerts.Create(_customAlert);
      
            await _dbUnit.SaveChangesAsync();
            return item;


        }

        public async Task<bool> DeleteAsync(int id)
        {
            CustomAlert obj = _dbUnit.customalerts.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return false;
            }
            _dbUnit.customalerts.Delete(obj);
            _dbUnit.SaveChangesAsync();
            return true;
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomAlertDto>> GetAllAsync()
        {
            var customalerts = _dbUnit.customalerts.GetAll().ToList();
            var customalertsDto = new List<CustomAlertDto>();
            foreach (var item in customalerts)
            {
                var customalertDto = new CustomAlertDto();
                customalertDto.CustomAlertName = item.CustomAlertName;
                customalertDto.WellName = item.WellName;
                customalertDto.Operator = item.Operator;
                customalertDto.Priority = item.Priority;
                customalertDto.Category = item.Category;
               // customalertDto.TimeandDate = item.TimeandDate;
                customalertDto.Value = item.Value;

                //customalertDto.WellId = item.WellId;

                customalertsDto.Add(customalertDto);
            }
            return customalertsDto;
        }

        public Task<CustomAlertDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomAlertDto>> GetFromJsonFile()
        {
            var items = new List<CustomAlertDto>();
            using (StreamReader r = new StreamReader("JSON/AlertCustom.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<CustomAlertDto>>(json);
            }
            return items;
        }
        public async Task<CustomAlertDto> UpdateAsync(int id, CustomAlertDto item)
        {
            CustomAlert _customAlert = new CustomAlert();
            _customAlert.WellName = item.WellName;
            _customAlert.Id = item.Id;
            //_customAlert.WellId = item.WellId;
            _customAlert.Category = item.Category;
            _customAlert.CustomAlertName = item.CustomAlertName;
            _customAlert.IsActive = item.IsActive;
            _customAlert.NotificationType = item.NotificationType;
            _customAlert.Value = item.Value;
            _customAlert.Priority = item.Priority;

            _dbUnit.customalerts.Update(_customAlert);

            await _dbUnit.SaveChangesAsync();
            return item;
        }

        public async Task<CustomAlertDto> CreateAsyncAlertCustom(CustomAlertDto alertCustomDto)
        {
            var filePath = @"D:\Delfi-glo\GLO-Back\GasLift\Delfi.Glo.Api\JSON\AlertCustom.json";
            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(filePath);
            // De-serialize to object or create new list
            var alertCustomList = JsonConvert.DeserializeObject<List<CustomAlertDto>>(jsonData)
                                  ?? new List<CustomAlertDto>();
            // Add any new employees
            alertCustomList.Add(new CustomAlertDto()
            {
                //Id = alertCustomList.Count() + 1,
                Id = alertCustomList[alertCustomList.Count - 1].Id + 1,
               // WellId = alertCustomDto.WellId,
                WellName = alertCustomDto.WellName,
                CustomAlertName = alertCustomDto.CustomAlertName,
                Category = alertCustomDto.Category,
                Priority = alertCustomDto.Priority,
                NotificationType = alertCustomDto.NotificationType,
                Operator = alertCustomDto.Operator,
                Value = alertCustomDto.Value,
                IsActive = alertCustomDto.IsActive
                //DateAndTime = alertCustomDto.DateAndTime
            });



            // Update json data string
            jsonData = JsonConvert.SerializeObject(alertCustomList, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            return alertCustomDto;
        }

        public async Task<bool> DeleteAsyncAlertCustom(int id)
        {
            var filePath = @"D:\Delfi-glo\GLO-Back\GasLift\Delfi.Glo.Api\JSON\AlertCustom.json";
            // Read existing json data
            var jsonData = System.IO.File.ReadAllText(filePath);
            // De-serialize to object or create new list
            var alertCustomList = JsonConvert.DeserializeObject<List<CustomAlertDto>>(jsonData)
                                  ?? new List<CustomAlertDto>();
            // Add any new employees
            var obj = alertCustomList.FirstOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }
            alertCustomList.Remove(obj);

            // Update json data string
            jsonData = JsonConvert.SerializeObject(alertCustomList, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            return true;
        }

        public async Task<bool> UpdateAsyncAlertCustom(int id, bool check)
        {
            var filePath = @"D:\Delfi-glo\GLO-Back\GasLift\Delfi.Glo.Api\JSON\AlertCustom.json";
            
            var jsonData = System.IO.File.ReadAllText(filePath);
            
            var alertCustomList = JsonConvert.DeserializeObject<List<CustomAlertDto>>(jsonData)
                                  ?? new List<CustomAlertDto>();
            
            var obj = alertCustomList.FirstOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }
            obj.IsActive = check;
            jsonData = JsonConvert.SerializeObject(alertCustomList, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            return true;
        }

        public async Task<CustomAlertDto> GetAlertCustomByAlertId(int id)
        {
            var items = new List<CustomAlertDto>();
            using (StreamReader r = new StreamReader("JSON/AlertCustom.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<CustomAlertDto>>(json);

            }
            var obj = items.Where(a=>a.Id == id).FirstOrDefault();
            return obj;
        }
    }
}
