using Newtonsoft.Json;

namespace OurSolarSystemAPI.Utility 
{
    public class ObjectToDictConverter 
    {
    public static Dictionary<string, object> ConvertToDictionary(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
    }
    }
}