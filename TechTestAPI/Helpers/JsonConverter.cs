using Newtonsoft.Json;

namespace TechTestAPI.Helpers
{
    public static class JsonConverter
    {
        public static T ConvertJsonToObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static string ConvertObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}