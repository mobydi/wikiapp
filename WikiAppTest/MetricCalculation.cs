using System.Linq;
using SimMetrics.Net.API;
using SimMetrics.Net.Metric;
using WikiApp.Models;
using Xunit;
using WikiApp.WikiApi;
using Xunit.Abstractions;

namespace WikiAppTest
{
    public class MetricCalculation
    {
        private readonly ITestOutputHelper output;

        public MetricCalculation(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void Verbose_With_Levenstein()
        {
            const double latitude = 37.786971;
            const double longitude = -122.399677;
            
            IStringMetric metric = new Levenstein();
            ITokenizer tokenizer = new Tokenizer();
	        
            var geopages = WikiApi.Geosearch(latitude, longitude);
            foreach (var geopage in geopages)
            {
                output.WriteLine($"[{geopage.Pageid}] {geopage.Title}");
				
                var images = WikiApi.Images(geopage.Pageid);
           
                var imagesWithMetrics = images
                    .Select(image => new {Image = image, Similarity = image.CalcMetric(geopage.Title, tokenizer, metric)})
                    .ToList();
                var title = imagesWithMetrics.Aggregate((image, next) => next.Similarity > image.Similarity ? next : image);

                output.WriteLine($"\t*[{title.Image.Ns}] {title.Image.Title} {title.Similarity}");

                foreach (var image in imagesWithMetrics)
                {
                    output.WriteLine($"\t[{image.Image.Ns}] {image.Image.Title} {image.Similarity}");
                }
            }
        }
    }
}
