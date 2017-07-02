using Xunit;
using WikiApp.WikiApi;

namespace WikiAppTest
{
    public class WikiApiImageTests
    {
        [Fact]
        public void Should_Work()
        {
			const int pageid = 40413203;
            var images = WikiApi.Images(pageid);
            
            Assert.NotNull(images);
            Assert.NotEmpty(images);
        }
        
        [Fact]
        public void Should_Work_Empty_With_Missing_Page()
        {
            const int pageid = 1;
            var images = WikiApi.Images(pageid);
            
            Assert.NotNull(images);
            Assert.NotEmpty(images);
        }
        
        [Fact]
        public void Should_Work_Multi_Page()
        {
            const int pageid1 = 40413203;
            const int pageid2 = 40377676;
            var images = WikiApi.Images(new []{pageid1, pageid2});
            
            Assert.NotNull(images);
            Assert.NotEmpty(images);
            
            Assert.NotEmpty(images[pageid1].Images);
            Assert.NotEmpty(images[pageid2].Images);
        }
    }
}
