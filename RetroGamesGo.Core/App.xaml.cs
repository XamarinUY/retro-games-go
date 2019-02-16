namespace RetroGamesGo.Core
{
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Xamarin.Forms;

    /// <summary>
    /// Main app class
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // App center analytics and crash reporting
            AppCenter.Start("android=b4b2a83b-8701-4182-bfa2-45e6e63a1356;" + "ios=f65ff0ff-72c5-4996-9ff7-83b8f515466b", typeof(Analytics), typeof(Crashes));
        }
    }
}