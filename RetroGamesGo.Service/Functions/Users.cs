using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RetroGamesGo.Models;
using RetroGamesGo.Service.Entities;
using RetroGamesGo.Service.Helpers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RetroGamesGo.Service.Functions
{
    public static class Users
    {
        /// <summary>
        /// LoginAsync
        /// </summary>      
        [FunctionName("AddUserAsync")]
        public static async Task<IActionResult> AddUserAsync([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "v1/User")]
            HttpRequest request, CancellationToken token, ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var user = JsonConvert.DeserializeObject<User>(requestBody);

                await SaveUser(user);

                return new OkObjectResult(new ServiceResponse<User>
                {
                    Success = true,
                    Data = user,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex, "Responses.AddUserAsync");
                return HttpHelper.BadRequestResult(ex.Message);
            }
        }

        #region --- SaveUser ---
        /// <summary>
        /// Save user in data storage
        /// </summary>
        /// <param name="user"></param>
        /// <param name="storageHelper"></param>
        /// <returns></returns>
        private static async Task SaveUser(User user)
        {
            try
            {
                var storageHelper = new StorageHelper(ConfigurationHelper.Configuration);
                var userEntity = new UserEntity(user.Email)
                {
                    Name = user.Name,
                    Document = user.Document,
                    CellPhone = user.CellPhone, 
                    Country = user.Country
                };
                await storageHelper.AddUpdateAsync("Users", userEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
