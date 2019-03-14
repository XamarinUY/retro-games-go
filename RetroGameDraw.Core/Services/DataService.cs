using Newtonsoft.Json;
using RetroGameDraw.Core.Interfaces;
using RetroGamesGo.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RetroGameDraw.Core.Services
{
    /// <summary>
    /// Data service methods
    /// </summary>
    public class DataService : IDataService
    {
        #region --- Variables ---

        private static HttpClient httpClient;

        #endregion

        #region --- Constructors ---

        public DataService()
        {
        }

        #endregion

        #region --- CreateHttpClient ---

        /// <summary>
        /// Creates and configures the HTTP client
        /// </summary>
        private static HttpClient CreateHttpClient()
        {
            if (httpClient != null) return httpClient;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add(AppConstants.XFunctionsKeyHeader, AppConstants.XFunctionsKey);
            httpClient.Timeout = new TimeSpan(0, 0, 5, 0);
            return httpClient;
        }

        #endregion


        #region --- GetAsync ---

        /// <summary>
        /// Executes a GET method to the service
        /// </summary>
        protected static async Task<T> GetAsync<T>(string uri, JsonSerializerSettings settings = null)
        {
            CreateHttpClient();
            var result = await httpClient.GetAsync(uri);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Error in GetAsync: {result.StatusCode}");
            }
            var response = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(response, settings);
            return data;
        }
        #endregion

        #region --- GetUsers ---
        /// <summary>
        /// Get users from remote service
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var response = await GetAsync<ServiceResponse<List<User>>>(DataServiceConstants.GetUsersUri);
                return response.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region --- GetWinnerUser ---
        /// <summary>
        /// Get winner user from remote service
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetWinnerUserAsync()
        {
            try
            {
                var response = await GetAsync<ServiceResponse<User>>(DataServiceConstants.GetWinnerUserUri);
                return response.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
