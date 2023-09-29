using Delfi.Glo.Entities.Dto;
using Newtonsoft.Json;
using System.Text.Json;

namespace Delfi.Glo.Common.Services
{
    // add common services here in this folder
    public static class UtilityService 
    {
        public static T? Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(text);
        }
    

        public static bool Write<T>(List<T> list, string filePath)
        {
           var jsonData = JsonConvert.SerializeObject(list, Formatting.Indented);
           File.WriteAllText(filePath, jsonData);
            return true;
        }

    }
}
