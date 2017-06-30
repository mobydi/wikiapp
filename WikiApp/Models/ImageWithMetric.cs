using WikiApp.WikiApi.Models.Imagesearch;

namespace WikiApp.Models
{
    class ImageWithMetric : Image
    {
        public new int Ns { get; }

        public new string Title { get;  }

        public double Similarity { get; }

        public ImageWithMetric(Image image, double similarity)
        {
            Ns = image.Ns;
            Title = image.Title;
            Similarity = similarity;
        }
    }
}
