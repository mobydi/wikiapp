using System.Collections.Generic;
using Newtonsoft.Json;

namespace WikiApp.WikiApi.Models.Geosearch
{
    public class Geosearch
    {
        [JsonProperty("pageid")]
        public int Pageid { get; set; }

        [JsonProperty("ns")]
        public int Ns { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("dist")]
        public double Dist { get; set; }

        [JsonProperty("primary")]
        public string Primary { get; set; }
    }

    public class Query
    {
        [JsonProperty("geosearch")]
        public IList<Geosearch> Geosearch { get; set; }
    }

    public class Response
    {
        [JsonProperty("batchcomplete")]
        public string Batchcomplete { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
    }
}
