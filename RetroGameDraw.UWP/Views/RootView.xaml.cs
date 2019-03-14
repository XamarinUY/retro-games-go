using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using RetroGameDraw.Core.ViewModels;
using System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;

namespace RetroGameDraw.UWP.Views
{
    [MvxViewFor(typeof(RootViewModel))]
    [MvxPagePresentation]
    public sealed partial class RootView : RootViewPage
    {
        public RootView()
        {
            this.InitializeComponent();
            var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();

            ApplicationViewTitleBar titleBar = view.TitleBar;
            if (titleBar != null)
            {
                var color = GetSolidColorBrush();

                titleBar.BackgroundColor = color;   // El color de fondo

                titleBar.ForegroundColor = Colors.White;    // El color del título (nombre)

                titleBar.ButtonBackgroundColor = color;    // El color de fondo de los botones minimizar/maximizar/cerrar

                titleBar.ButtonForegroundColor = Colors.White;    // El color de los botones mencionados

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

    public abstract class RootViewPage : MvxWindowsPage<RootViewModel>
    {
    }
}
