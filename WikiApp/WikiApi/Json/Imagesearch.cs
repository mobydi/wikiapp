using System.Collections.Generic;
using Newtonsoft.Json;

namespace WikiApp.WikiApi.Json.Imagesearch
{
    public class Image
    {
        [JsonProperty("ns")]
        public int Ns { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class Page
    {
        [JsonProperty("pageid")]
        public int Pageid { get; set; }

        [JsonProperty("ns")]
        public int Ns { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }
    
    public class Query
    {
        [JsonProperty("pages")]
        public Dictionary<int, Page> Pages { get; set; }
    }

    public class Continue
    {
        [JsonProperty("imcontinue")]
        public string Imcontinue { get; set; }

        [JsonProperty("continue")]
        public string ContinueSep { get; set; }
    }

    public class Response
    {
        [JsonProperty("batchcomplete")]
        public string Batchcomplete { get; set; }

        [JsonProperty("continue")]
        public Continue Continue { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
        
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
