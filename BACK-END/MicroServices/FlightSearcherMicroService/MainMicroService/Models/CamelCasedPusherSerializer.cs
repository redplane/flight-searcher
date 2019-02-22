using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PusherServer;

namespace MainMicroService.Models
{
    public class CamelCasedPusherSerializer : ISerializeObjectsToJson
    {
        /// <inheritDoc />
        public string Serialize(object objectToSerialize)
        {
            var camelCasedSerializerSettings = new JsonSerializerSettings();
            camelCasedSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(objectToSerialize, camelCasedSerializerSettings);
        }
    }
}