namespace RetroGamesGo.Core
{
    using MvvmCross.IoC;
    using MvvmCross.ViewModels;    
    using ViewModels;
    using Services;
    using MvvmCross;
    using MvvmCross.Plugin.JsonLocalization;
    using RetroGamesGo.Core.Helpers;


    /// <summary>
    /// MvvmCross Application
    /// </summary>
    public class MvxApp : MvxApplication
    {
        /// <summary>
        /// Initialize the services and defines the first viewmodel
        /// </summary>
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();   
                     
            if(!Settings.OnboardingShown)
                RegisterAppStart<OnboardingViewModel>();
            else
                RegisterAppStart<MainViewModel>();

            InitializeTextProvider();
        }


        /// <summary>
        /// Initializes the localization provider
        /// </summary>
        private void InitializeTextProvider()
        {
            var builder = new TextProviderBuilder();
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProviderBuilder>(builder);
            Mvx.IoCProvider.RegisterSingleton(builder.TextProvider);
        }

    }
}
