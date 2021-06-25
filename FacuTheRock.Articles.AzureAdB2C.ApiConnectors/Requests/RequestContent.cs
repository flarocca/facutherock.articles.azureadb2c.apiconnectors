using Newtonsoft.Json;

namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Requests
{
    public class RequestContent
    {
        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "email")]
        public string Email { get; set; }
    }
}
