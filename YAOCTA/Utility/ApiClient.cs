﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace YAOCTA.Utility
{
    public static class ApiClient
    {
        private const string CorsOverride = "https://cors-anywhere.herokuapp.com";
        private const string ApiEndpoint = "https://api.octranspo1.com/v1.3";
        private const string AppId = "da9f743d";
        private const string ApiKey = "b9f53f3f55876acad1d5c2b67fe41233";
        private const string OriginHeader = "Origin";
        private const string DomainName = "https://minimalistoctranspo.azurewebsites.net";
        private const string XRequestedWithParameter = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        private static readonly HttpClient client = new HttpClient();

        public static async Task<T> SendRequest<T>(string endpoint, IDictionary<string, string> parameters)
        {
            var requestUri = String.Format("{0}/{1}/{2}?appID={3}&apiKey={4}&format=JSON", CorsOverride, ApiEndpoint, endpoint, AppId, ApiKey);
            foreach (var key in parameters.Keys)
            {
                requestUri += String.Format("&{0}={1}", key, parameters[key]);
            }
            var content = new StringContent("");
            content.Headers.Add(OriginHeader, DomainName);
            content.Headers.Add(XRequestedWithParameter, XmlHttpRequest);
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
