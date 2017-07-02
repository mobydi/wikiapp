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
	        IStringMetric metric = new Levenstein();
	        ITokenizer tokenizer = new Tokenizer();
	        
	        var result = WikiApi.WikiApi.Geosearch(Latitude, Longitude)
		        .AsParallel()
		        .Select(p => new
		        {
			        Page = p,
			        Title = WikiApi.WikiApi.Images(p.Pageid)[p.Pageid].Images
				        .Select(image => new {Image = image, Similarity = image.CalcMetric(p.Title, tokenizer, metric)})
				        .Aggregate((image, next) => next.Similarity > image.Similarity ? next : image)
		        })
		        .ToList();

	        foreach (var page in result)
	        {
		        Console.WriteLine($"[{page.Page.Pageid}] {page.Page.Title}");
			
		        Console.WriteLine($"\t Image title: [{page.Title.Image.Title}]");
	        }
        }
    }
}