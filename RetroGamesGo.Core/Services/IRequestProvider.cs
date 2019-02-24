using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetroGamesGo.Core.Services
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "", Dictionary<string, string> headers = null);
        Task<TResult> PostAsync<TResult>(string uri, object data, string token = "", Dictionary<string, string> headers = null);
    }
}
