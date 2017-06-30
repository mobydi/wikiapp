using System;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
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
            var commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);

            CommandOption latitude = commandLineApplication.Option(
                "-lat | --latitude <value>",
                "Get wikipedia articles nearest to that latitude",
                CommandOptionType.SingleValue);

            CommandOption longitude = commandLineApplication.Option(
                "-lon | --longitude <value>",
                "Get wikipedia articles nearest to that longitude",
                CommandOptionType.SingleValue);

            CommandOption verbose = commandLineApplication.Option(
                "-v | --verbose", "Display debug information.",
                CommandOptionType.NoValue);

            commandLineApplication.HelpOption("-? | -h | --help");

            commandLineApplication.OnExecute(() =>
            {
                if (true)
                {
                    Action(verbose.HasValue());
                }
                else
                {
                    commandLineApplication.ShowHelp();
                }
                return 0;
            });

            commandLineApplication.Execute(args);

            Console.ReadLine();
        }


        public static void Action(bool verbose)
        {
            var geopages = WikiApi.WikiApi.Geosearch(Latitude, Longitude);
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

                    if (verbose)
                    {
                        imagesWithMetrics.ForEach(image => Console.WriteLine(string.Format("\t[{0}] {1} {2}", image.Ns, image.Title, image.Similarity)));
                    }
                }
            }
        }
    }
}