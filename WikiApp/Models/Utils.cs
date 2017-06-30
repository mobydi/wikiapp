using SimMetrics.Net.API;
using WikiApp.WikiApi.Models.Imagesearch;

namespace WikiApp.Models
{
    static partial class Utils
    {
        public static ImageWithMetric AddMetric(this Image image, string title, IStringMetric metric)
        {
            var similarity = metric.GetSimilarity(title.Stem().ToLower(), image.Title.ToLower());
            return new ImageWithMetric(image, similarity);
        }

        public static string Stem(this string title)
        {
            return title.Replace("File:", string.Empty);
        }
    }
}