using Newtonsoft.Json;

namespace WikiApp.WikiApi.Json
{
    public class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }
    }

}