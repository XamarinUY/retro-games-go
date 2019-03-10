namespace RetroGamesGo.Droid.Activities
{
    using Android;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;
    using Android.App;
    using Android.Content.PM;
    using MvvmCross.Forms.Platforms.Android.Views;
    using Android.OS;
    using Core.ViewModels;
    using Acr.UserDialogs;
    using Lottie.Forms.Droid;

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

        public static MainActivity Instance { get; private set; }

        /// <summary>
        /// Setups resources
        /// </summary>
        protected override void OnCreate(Bundle bundle)
        {
        
            ToolbarResource = Droid.Resource.Layout.Toolbar;
            TabLayoutResource = Droid.Resource.Layout.Tabbar;
            base.OnCreate(bundle);
            UserDialogs.Init(this);
            Instance = this;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);            
            RequestCameraPermission();
            
          
            RequestCameraPermission();            
        }


        /// <summary>
        /// Forms and plugins initialization
        /// </summary>        
        public override void InitializeForms(Bundle bundle)
        {
            base.InitializeForms(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            AnimationViewRenderer.Init();
        }


        /// <summary>
        /// Request the camera permission
        /// </summary>
        private void RequestCameraPermission()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.Camera }, 42);                
            }
        }
    }
}

