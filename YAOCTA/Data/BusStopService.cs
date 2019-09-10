using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace YAOCTA.Data
{
    public class BusStopService
    {
        // constants for http requests, will probably be abstracted into own class
        private const string corsOverride = "https://cors-anywhere.herokuapp.com";
        private const string apiEndpoint = "https://api.octranspo1.com/v1.3";
        private const string appId = "da9f743d";
        private const string apiKey = "b9f53f3f55876acad1d5c2b67fe41233";

        private static readonly HttpClient client = new HttpClient();

        // TODO: move this to utility class
        public async Task<T> SendRequest<T>(string endpoint, IDictionary<string, string> parameters)
        {
            var requestUri = String.Format("{0}/{1}/{2}?appID={3}&apiKey={4}&format=JSON", corsOverride, apiEndpoint, endpoint, appId, apiKey);
            foreach (var key in parameters.Keys)
            {
                requestUri += String.Format("&{0}={1}", key, parameters[key]);
            }
            var content = new StringContent("");
            content.Headers.Add("Origin", "https://octranspostoptimes.azurewebsites.net");
            content.Headers.Add("X-Requested-With", "XMLHttpRequest");
            var response = await client.PostAsync(requestUri, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
