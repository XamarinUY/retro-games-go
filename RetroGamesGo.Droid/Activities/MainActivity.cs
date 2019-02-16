using Lottie.Forms.Droid;
namespace RetroGamesGo.Droid.Activities
{   
    using Android.App;
    using Android.Content.PM;
    using MvvmCross.Forms.Platforms.Android.Views;
    using Android.OS;
    using Core.ViewModels;

    /// <summary>
    /// Main activity 
    /// </summary>
    [Activity(
        Theme = "@style/AppTheme",
        Label = "@string/appName",
        Icon = "@mipmap/ic_launcher",
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<StartUpViewModel>
    {
        /// <summary>
        /// Setups resources
        /// </summary>
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Droid.Resource.Layout.Toolbar;
            TabLayoutResource = Droid.Resource.Layout.Tabbar;
            base.OnCreate(bundle);
            AnimationViewRenderer.Init();
        }


        /// <summary>
        /// Forms and plugins initialization
        /// </summary>        
        public override void InitializeForms(Bundle bundle)
        {
            base.InitializeForms(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
        }
    }
}

