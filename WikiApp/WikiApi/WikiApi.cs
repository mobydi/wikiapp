using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WikiApp.WikiApi.Json.Geosearch;
using WikiApp.WikiApi.Json.Imagesearch;

namespace WikiApp.WikiApi
{
    public static class WikiApi
    {
        public static IList<Geosearch> Geosearch(double latitude, double longitude)
        {
            var lat = latitude.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
            var lng = longitude.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
           
            return Internal.Geosearch(lat, lng);
        }
	    
	    public static IDictionary<int, Page> Images(int pageid)
	    {
		    var pid = pageid.ToString(System.Globalization.CultureInfo.InvariantCulture);
		    
	        return Internal.Images(pid);
        }

        public static IDictionary<int, Page> Images(IEnumerable<int> pageids)
        {
	        string pids = string.Join("|", pageids.Select(p => p.ToString()));
	        
            return Internal.Images(pids);
        }

	    internal static class Internal
	    {
		    const string Api = "https://en.wikipedia.org/w/api.php";
	        
            internal static IDictionary<int, Page> Images(string pageid)
	        {
		        string url = $"{Api}?action=query&prop=images&pageids={pageid}&format=json";

		        var result = new Dictionary<int, Json.Imagesearch.Page>();
		        var serializer = new JsonSerializer();

		        var response = url.Get<Json.Imagesearch.Response>(serializer);
		        if (response.Error != null)
			        throw new ApiException(response.Error.Code, response.Error.Info);
		        
		        result.Zip(response.Query.Pages);

		        while (response.Continue != null)
		        {
			        string imurl = $"{url}&imcontinue={response.Continue.Imcontinue}";
			        response = imurl.Get<Json.Imagesearch.Response>(serializer);    
			        if (response.Error != null)
				        throw new ApiException(response.Error.Code, response.Error.Info);
			        
			        result.Zip(response.Query.Pages);
		        }
		        return result;
	        }
	        
	        internal static IList<Geosearch> Geosearch(string lat, string lng)
	        {
		        string url = $"{Api}?action=query&list=geosearch&gsradius=10000&gscoord={lat}|{lng}&gslimit=50&format=json";

		        JsonSerializer serializer = new JsonSerializer();
		        var response = url.Get<Json.Geosearch.Response>(serializer);
		        
		        if (response.Error != null)
			        throw new ApiException(response.Error.Code, response.Error.Info);

		        return response.Query.Geosearch;
	        }
        }

    }
}
