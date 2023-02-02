using System.Net;

namespace MonitorSite.Helpers
{
    public class Requesters
    {
        public static async Task<HttpStatusCode> GetStatusFromUrl(string url)
        {
            try
            {
                HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(url);
                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.NotFound;
            }
        }
    }
}
