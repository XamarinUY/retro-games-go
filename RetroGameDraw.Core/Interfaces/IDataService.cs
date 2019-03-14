using RetroGamesGo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroGameDraw.Core.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Get users
        /// </summary>
        Task<List<User>> GetUsersAsync();

        /// <summary>
        /// Get Winner user 
        /// </summary>
        Task<User> GetWinnerUserAsync();
    }
}
