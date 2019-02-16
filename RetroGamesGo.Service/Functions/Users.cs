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
using System.Linq;

namespace RetroGamesGo.Service.Functions
{
    public static class Users
    {
        #region --- AddUserAsync ---
        /// <summary>
        /// Save Sended User in storage
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
        #endregion


        #region --- SaveUser ---
        /// <summary>
        /// Save user in data storage
        /// </summary>
        /// <param name="user"></param>
        /// <param name="storageHelper"></param>
        /// <returns></returns>
        private static async Task SaveUser(User user, bool winner = false)
        {
            try
            {
                var storageHelper = new StorageHelper(ConfigurationHelper.Configuration);
                var localUser = await storageHelper.GetItemAsync<UserEntity>("Users", "Users", user.Email);
                winner = localUser != null ? localUser.Winner : winner;

                var userEntity = new UserEntity(user.Email)
                {
                    Name = user.Name,
                    Document = user.Document,
                    CellPhone = user.CellPhone,
                    Country = user.Country,
                    Winner = winner
                };
                await storageHelper.AddUpdateAsync("Users", userEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region --- GetWinnerUser ---
        /// <summary>
        /// Get from storage winner user
        /// </summary>
        /// <param name="req"></param>
        /// <param name="token"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        [FunctionName("GetWinnerUser")]
        public static async Task<IActionResult> GetLocationsASync(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "v1/user/winner")] HttpRequest req,
           CancellationToken token,
           ILogger logger)
        {
            try
            {
                var storageHelper = new StorageHelper(ConfigurationHelper.Configuration);
                var users = await storageHelper.GetItemsAsync<UserEntity>("Users");
                var usersSet = users.Where(x => !x.Winner).Select(g => g).ToList();
                var user = new User();
                if (usersSet.Count() >= 1)
                {
                    Random rnd = new Random();
                    var userEntity = usersSet.ElementAt(rnd.Next(0, usersSet.Count()));
                    user = new User
                    {
                        Email = userEntity.Email,
                        Document = userEntity.Document,
                        Name = userEntity.Name,
                        CellPhone = userEntity.CellPhone,
                        Country = userEntity.Country,
                    };

                    await SaveUser(user, true);
                }

                return new OkObjectResult(new ServiceResponse<User>
                {
                    Success = true,
                    Data = user,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex, "Locations.GetLocationsAsync");
                return HttpHelper.BadRequestResult(ex.Message);
            }
        }
        #endregion
    }
}
