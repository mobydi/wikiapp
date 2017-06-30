using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace WikiApp.WikiApi
{
    static class WikiApi
    {
        public static IList<Models.Geosearch.Geosearch> Geosearch(string lat, string lng)
        {
            string url = $"https://en.wikipedia.org/w/api.php?action=query&list=geosearch&gsradius=10000&gscoord={lat}|{lng}&gslimit=50&format=json";

            JsonSerializer serializer = new JsonSerializer();
            return url.Get<Models.Geosearch.Response>(serializer).Query.Geosearch;
        }

        public static IDictionary<int, Models.Imagesearch.Page> Images(int pageid)
        {
            string url = $"https://en.wikipedia.org/w/api.php?action=query&prop=images&pageids={pageid}&format=json";

            var result = new Dictionary<int, Models.Imagesearch.Page>();
            var serializer = new JsonSerializer();

            var response = url.Get<Models.Imagesearch.Response>(serializer);
            result.Zip(response.Query.Pages);

            while (response.Continue != null)
            {
                string imurl = $"{url}&imcontinue={response.Continue.Imcontinue}";
                response = imurl.Get<Models.Imagesearch.Response>(serializer); 
                result.Zip(response.Query.Pages);
            }
            return result;
        }

        public static IDictionary<int, Models.Imagesearch.Page> Images(List<int> pages)
        {
            string pageid = string.Join("|", pages.Select(p => p.ToString()));
            string url = $"https://en.wikipedia.org/w/api.php?action=query&prop=images&pageids={pageid}&format=json";

            throw new NotImplementedException();
        }
        
        private static T Get<T>(this string url, JsonSerializer serializer)
        {
            using (HttpClient client = new HttpClient())
            using (Stream s = client.GetStreamAsync(url).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(reader);
            }
        }
    }
}
