using System;
using System.Collections.Generic;
using System.Text;

namespace RetroGamesGo.Models
{
    public class User
    {
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CI
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Cell Phone
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
    }
}
