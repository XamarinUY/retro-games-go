namespace RetroGamesGo.iOS
{
    using MvvmCross;
    using MvvmCross.Base;
    using MvvmCross.Forms.Platforms.Ios.Core;    
    using MvvmCross.Logging;
    using MvvmCross.Plugin.Json;
    using Core;
    using RetroGamesGo.Core.Repositories;

    /// <summary>
    /// MvvMCross iOS Setup
    /// </summary>
    public class Setup : MvxFormsIosSetup<MvxApp, App>
    {
        /// <summary>
        /// Sets the log provider
        /// </summary>
        /// <returns></returns>
        public override MvxLogProviderType GetDefaultLogProviderType()
            => MvxLogProviderType.Console;


        /// <summary>
        /// Initializes the platform services
        /// </summary>
        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton<IMvxJsonConverter>(new MvxJsonConverter());
            Mvx.IoCProvider.RegisterSingleton<IDatabaseConnection>(new SQLiteClient());
            base.InitializeFirstChance();
        }
    }


}