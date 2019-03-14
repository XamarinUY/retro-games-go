using System;
using System.Threading.Tasks;

namespace RetroGameDraw.Core.Interfaces
{
    /// <summary>
    /// Platform service interface
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// Shows a confirmation message
        /// </summary>        
        Task ConfirmAsync(string title, string message,
            string okText, string cancelText, Action<bool> callback);

        /// <summary>
        /// NotifyAsync
        /// </summary>
        Task NotifyAsync(string title, string message, string cancelText);
    }
}
