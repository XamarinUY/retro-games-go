using System;
using System.Collections.Generic;
using System.Text;

namespace RetroGameDraw.Core
{
    /// <summary>
    /// Constants users by the data service
    /// </summary>
    public static class AppConstants
    {
        public const string BaseUri = "https://retrogamego.azurewebsites.net/api/v1/";

        public const string XFunctionsKeyHeader = "x-functions-key";

        public const string XFunctionsKey = "pliPSu/ft9HPQzaVMktIB7dQmh8npBV9lA0Ecc1nrAMlgthXguiBDw==";
    }

    /// <summary>
    /// Constants users by the data service
    /// </summary>
    public static class DataServiceConstants
    {

        public const string GetUsersUri = AppConstants.BaseUri + "Users";

        public const string GetWinnerUserUri = AppConstants.BaseUri + "User/winner";

    }
}
