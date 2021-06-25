using Newtonsoft.Json;

namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Responses
{
    public abstract class ResponseContentBase
    {
        protected const string DefaultApiVersion = "1.0.0";

        protected ResponseContentBase(
            string action,
            string userMessage,
            string version = DefaultApiVersion)
        {
            Action = action;
            UserMessage = userMessage;
            Version = version;
        }

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "version")]
        public string Version { get; private set; }

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "action")]
        public string Action { get; private set; }

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "userMessage")]
        public string UserMessage { get; private set; }
    }
}
