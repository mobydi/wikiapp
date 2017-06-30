using System.Collections.Generic;

namespace WikiApp.WikiApi
{
    static class Utils
    {
        internal static void Zip(this Dictionary<int, Models.Imagesearch.Page> dictionary, Dictionary<int, Models.Imagesearch.Page> with)
        {
            foreach (KeyValuePair<int, Models.Imagesearch.Page> keyValuePair in with)
            {
                Models.Imagesearch.Page page;
                if (dictionary.TryGetValue(keyValuePair.Key, out page))
                {
                    if (page.Images == null)
                        page.Images = new List<Models.Imagesearch.Image>();

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
}
