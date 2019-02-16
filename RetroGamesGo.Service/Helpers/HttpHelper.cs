using Microsoft.AspNetCore.Mvc;
using RetroGamesGo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroGamesGo.Service.Helpers
{
    public class HttpHelper
    {
        #region --- BadRequests ---

        public static BadRequestObjectResult BadRequestResult<T>(string message) where T : class
        {
            return new BadRequestObjectResult(new ServiceResponse<T>() { Success = false, Errors = new[] { message } });
        }


        public static BadRequestObjectResult BadRequestResult(string message)
        {
            return new BadRequestObjectResult(new ServiceResponse() { Success = false, Errors = new[] { message } });
        }

        #endregion
    }
}
