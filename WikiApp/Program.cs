using System;
using System.Linq;
using SimMetrics.Net.Metric;
using WikiApp.Models;

namespace WikiApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Downloading!");

            Action();

            Console.WriteLine("Done! Press Any Key To Exit...");
            Console.ReadLine();
        }


        public static void Action()
        {
            var geopages = WikiApi.WikiApi.Geosearch("37.786971", "-122.399677");
            var metric = new Levenstein();
            
            foreach (var geopage in geopages)
            {
                var images = WikiApi.WikiApi.Images(geopage.Pageid);

                foreach (var page in images.Values)
                {
                    Console.WriteLine($"[{page.Pageid}] {page.Title}");

                    var imagesWithMetrics = page.Images.Select(i => i.AddMetric(page.Title, metric)).ToList();
                    var bestTitle = imagesWithMetrics.Aggregate((image, next) => next.Similarity > image.Similarity ? next : image);

                    Console.WriteLine($"\t*[{bestTitle.Ns}] {bestTitle.Title} {bestTitle.Similarity}");
                    foreach (var image in imagesWithMetrics)
                    {
                        Console.WriteLine(string.Format("\t[{0}] {1} {2}", image.Ns, image.Title, image.Similarity));
                    }
                }
            }
        }
    }
}