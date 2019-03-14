using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGameDraw.UWP.Services
{
    #region --- Usings ---

    using System;
    using RetroGameDraw.Core.Interfaces;
    using RetroGameDraw.Core.Services;
    using Windows.UI.Xaml.Controls;
    #endregion

    public class PlatformService : IPlatformService
    {
        #region --- ConfirmAsync ---
        /// <summary>
        /// Shows a confirmation message
        /// </summary>        
        public async Task ConfirmAsync(string title, string message, string okText, string cancelText,
            Action<bool> callback)
        {
            try
            {
                var locationPromptDialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    SecondaryButtonText = cancelText,
                    PrimaryButtonText = okText
                };

                var result = await locationPromptDialog.ShowAsync();
                callback(result == ContentDialogResult.Primary);
            }
            catch
            {
                //Ignored
            }
        }

        #endregion


        #region --- NotifyAsync ---
        /// <summary>
        /// Shows a confirmation message
        /// </summary>        
        public async Task NotifyAsync(string title, string message, string okText)
        {
            try
            {
                var locationPromptDialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    PrimaryButtonText = okText
                };

                await locationPromptDialog.ShowAsync();
            }
            catch
            {
                //Ignored
            }
        }

        #endregion
    }
}
