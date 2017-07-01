using Xunit;
using WikiApp.WikiApi;

namespace WikiAppTest
{
    public class MetricCalculation
    {
        [Fact]
        public void Should_Work()
        {
			const int pageid = 40413203;
            var images = WikiApi.Images(pageid);
            
            Assert.NotNull(images);
            Assert.NotEmpty(images);
            Assert.NotEmpty(images[pageid].Images);
        }
        
       
        
    }
}
