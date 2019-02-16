using System;
using System.Collections.Generic;
using System.Text;

namespace RetroGamesGo.Models
{
    /// <summary>
    /// Service response envelope
    /// </summary>
    public class ServiceResponse<T> where T : class
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string[] Errors { get; set; }
    }



    /// <summary>
    /// Service response envelope
    /// </summary>
    public class ServiceResponse
    {
        public bool Success { get; set; }

        public string[] Errors { get; set; }
    }
}
