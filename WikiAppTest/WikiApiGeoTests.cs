using Xunit;
using WikiApp.WikiApi;

namespace WikiAppTest
{
    public class WikiApiGeoTests
    {
        [Fact]
        public void Should_Work()
        {
			const double latitude = 37.786971;
			const double longitude = -122.399677;
            var geopages = WikiApi.Geosearch(latitude, longitude);
            
            Assert.NotNull(geopages);
            Assert.NotEmpty(geopages);
        }
        
        [Fact]  
        public void Should_Fail_With_Wrong_Geo()
        {
            const double latitude = 307.786971;
            const double longitude = 200.399677;
            
            Assert.Throws<ApiException>(() => WikiApi.Geosearch(latitude, longitude));
        }
    }
}
