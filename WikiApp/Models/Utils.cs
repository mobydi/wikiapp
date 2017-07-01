using SimMetrics.Net.API;
using WikiApp.WikiApi.Json.Imagesearch;

namespace WikiApp.Models
{
    static class Utils
    {
        public static ImageWithMetric CalcMetric(this Image image, string title, ITokenizer tokenizer, IStringMetric metric)
        {
            var similarity = metric.GetSimilarity(title.ToLower(), string.Join(" ", image.Title).ToLower());
            return new ImageWithMetric(image, similarity);
        }
    }
}