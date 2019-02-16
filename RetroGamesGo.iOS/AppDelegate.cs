namespace RetroGamesGo.iOS
{
    using Foundation;
    using MvvmCross.Forms.Platforms.Ios.Core;
    using Core;
    using UIKit;

  
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
            // Todo: adjust values to match the selected color scheme
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(34, 51, 68);
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(255, 255, 255);
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.MakeKeyAndVisible();
            return base.FinishedLaunching(application, launchOptions);
        }


        protected override void LoadFormsApplication()
        {
            base.LoadFormsApplication();
            global::Xamarin.Forms.Forms.Init();
        }

    }
}

