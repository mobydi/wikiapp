using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace WikiApp.WikiApi
{
    internal static class Utils
    {
        internal static void Zip(this Dictionary<int, Json.Imagesearch.Page> destination, Dictionary<int, Json.Imagesearch.Page> with)
        {
            foreach (var withPair in with)
            {
                Json.Imagesearch.Page page;
                if (destination.TryGetValue(withPair.Key, out page))
                {
                    if (page.Images == null)
                        page.Images = new List<Json.Imagesearch.Image>();

                    if (withPair.Value.Images != null)
                        page.Images.AddRange(withPair.Value.Images);
                }
                else
                {
                    destination.Add(withPair.Key, withPair.Value);
                }
            }
        }
    }

    internal static class Http
    {
        internal static T Get<T>(this string url, JsonSerializer serializer)
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
