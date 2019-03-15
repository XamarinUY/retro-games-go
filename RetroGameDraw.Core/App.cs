namespace RetroGameDraw.Core
{
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Plugin.JsonLocalization;
    using MvvmCross.ViewModels;
    #region --- Usings --- 

    using Services;
    using System;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Main application
    /// </summary>
    public class App : MvxApplication
    {
        #region --- Initialize --- 
        /// <summary>
        /// Initialize the services and defines the first viewmodel
        /// </summary>
        public override void Initialize()
        {
            try
            {
                CreatableTypes()
                  .EndingWith("Service")
                  .AsInterfaces()
                  .RegisterAsLazySingleton();
                InitializeTextProvider();
                RegisterAppStart<ViewModels.RootViewModel>();

            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        #endregion


        #region --- InitializeTextProvider --- 
        /// <summary>
        /// Initializes the provider used for UI localization
        /// </summary>
        private void InitializeTextProvider()
        {
            var builder = new TextProviderBuilder();
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProviderBuilder>(builder);
            Mvx.IoCProvider.RegisterSingleton(builder.TextProvider);
        }

        #endregion


        /// <summary>
        /// Do any UI bound startup actions here
        /// </summary>
        public override Task Startup()
        {
            return base.Startup();
        }

        /// <summary>
        /// If the application is restarted (eg primary activity on Android 
        /// can be restarted) this method will be called before Startup
        /// is called again
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }
    }
}
