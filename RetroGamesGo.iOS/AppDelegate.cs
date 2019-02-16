namespace RetroGamesGo.iOS
{
    using Foundation;
    using MvvmCross.Forms.Platforms.Ios.Core;
    using Core;
    using UIKit;
    using Lottie.Forms.iOS.Renderers;


    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the
    /// User Interface of the application, as well as listening (and optionally responding) to application events from iOS. 
    /// </summary>
    [Register("AppDelegate")]
    public class AppDelegate : MvxFormsApplicationDelegate<Setup, MvxApp, App>
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(12, 16, 19);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.MakeKeyAndVisible();

            // Init plugins
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            AnimationViewRenderer.Init();
            return base.FinishedLaunching(application, launchOptions);
        }

        protected override void LoadFormsApplication()
        {
            base.LoadFormsApplication();
            global::Xamarin.Forms.Forms.Init();
        }
    }
}

