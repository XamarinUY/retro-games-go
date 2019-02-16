using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RetroGamesGo.Core.Utils;

namespace RetroGamesGo.Core.Services
{
    public class RequestProvider : IRequestProvider
    {
        private readonly JsonSerializerSettings serializerSettings;

        public RequestProvider()
        {
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "", Dictionary<string, string> headers = null)
        {
            System.Net.Http.HttpClient httpClient = CreateHttpClient(token);
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    AddHeaderParameter(httpClient, kvp.Key, kvp.Value);
                }
            }
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings));

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, object data, string token = "", Dictionary<string, string> headers = null)
        {
            System.Net.Http.HttpClient httpClient = CreateHttpClient(token);

            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    AddHeaderParameter(httpClient, kvp.Key, kvp.Value);
                }
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings));

            return result;
        }

        private void AddHeaderParameter(System.Net.Http.HttpClient httpClient, string header, string value)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(header))
                return;

            httpClient.DefaultRequestHeaders.Add(header, value);
        }

        private System.Net.Http.HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add(Constants.RequestProvider.HeaderName, Constants.RequestProvider.ApiKey);

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }

}
