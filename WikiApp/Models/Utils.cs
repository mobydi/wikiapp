using SimMetrics.Net.API;
using WikiApp.WikiApi.Json.Imagesearch;

namespace WikiApp.Models
{
    public static class Utils
    {
        public static double CalcMetric(this Image image, string title, ITokenizer tokenizer, IStringMetric metric)
        {
            var imageTitle = string.Join(" ", tokenizer.Tokenize(image.Title));
            var similarity = metric.GetSimilarity(title.ToLower(), imageTitle.ToLower());
            
            return similarity;
        }
    }
}