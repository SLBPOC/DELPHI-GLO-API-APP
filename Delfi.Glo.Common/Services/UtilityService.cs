using System.Text.Json;

namespace Delfi.Glo.Common.Services
{
    // add common services here in this folder
    public static class UtilityService 
    {
        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }



    }
}
