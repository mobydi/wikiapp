using System;
using System.Linq;
using SimMetrics.Net.API;
using SimMetrics.Net.Metric;
using WikiApp.Models;

namespace WikiApp
{
    class Program
    {
        const double Latitude = 37.786971;
        const double Longitude = -122.399677;

        static void Main(string[] args)
        {
            Action(true);

            Console.ReadLine();
        }


        static void Action(bool verbose)
        {
			var geopages = WikiApi.WikiApi.Geosearch(Latitude, Longitude);
            IStringMetric metric = new Levenstein();
	        ITokenizer tokenizer = new Tokenizer();

			foreach (var geopage in geopages)
			{
				var images = WikiApi.WikiApi.Images(geopage.Pageid);

				foreach (var page in images.Values)
				{
					Console.WriteLine($"[{page.Pageid}] {page.Title}");

					var imagesWithMetrics = page.Images.Select(i => i.CalcMetric(page.Title, tokenizer, metric)).ToList();
					var bestTitle = imagesWithMetrics.Aggregate((image, next) => next.Similarity > image.Similarity ? next : image);

					Console.WriteLine($"\t*[{bestTitle.Ns}] {bestTitle.Title} {bestTitle.Similarity}");

					if (verbose)
					{
						imagesWithMetrics.ForEach(image => Console.WriteLine(string.Format("\t[{0}] {1} {2}", image.Ns, image.Title, image.Similarity)));
					}
				}
			}
        }
    }
}