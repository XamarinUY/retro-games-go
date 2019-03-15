using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RetroGameDraw.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView
    {
        public MainView()
        {
            this.InitializeComponent();
            var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();

            ApplicationViewTitleBar titleBar = view.TitleBar;
            if (titleBar != null)
            {
                var color = GetSolidColorBrush();

                titleBar.BackgroundColor = color;   

                titleBar.ForegroundColor = Colors.White;    

                titleBar.ButtonBackgroundColor = color;    

                titleBar.ButtonForegroundColor = Colors.White;    

            }
        }

        /// <summary>
        /// Change windows bar color 
        /// </summary>
        /// <returns></returns>
        public Color GetSolidColorBrush()
        {
            try
            {
                SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(0, 18, 22, 25));
                return myBrush.Color;
            }
            catch
            {
                return Colors.Black;
            }
        }
    }
}
