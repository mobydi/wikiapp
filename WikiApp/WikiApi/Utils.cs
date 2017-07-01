using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace WikiApp.WikiApi
{
    internal static class Utils
    {
        internal static void Zip(this Dictionary<int, Json.Imagesearch.Page> dictionary, Dictionary<int, Json.Imagesearch.Page> with)
        {
            foreach (KeyValuePair<int, Json.Imagesearch.Page> keyValuePair in with)
            {
                Json.Imagesearch.Page page;
                if (dictionary.TryGetValue(keyValuePair.Key, out page))
                {
                    if (page.Images == null)
                        page.Images = new List<Json.Imagesearch.Image>();

                    if (keyValuePair.Value.Images != null)
                        page.Images.AddRange(keyValuePair.Value.Images);
                }
                else
                {
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
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
