using WikiApp.WikiApi.Json.Imagesearch;

namespace WikiApp.Models
{
    class ImageWithMetric
    {
        public int Ns { get; }

        public string Title { get;  }

        public double Similarity { get; }

        public ImageWithMetric(Image image, double similarity)
        {
            Ns = image.Ns;
            Title = image.Title;
            Similarity = similarity;
        }
    }
}
