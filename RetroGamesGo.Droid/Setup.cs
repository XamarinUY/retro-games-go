namespace RetroGamesGo.Droid
{
    using Core;
    using MvvmCross;
    using MvvmCross.Base;
    using MvvmCross.Forms.Platforms.Android.Core;
    using MvvmCross.Logging;
    using MvvmCross.Plugin.Json;
    using RetroGamesGo.Core.Repositories;

    /// <summary>
    /// Android setup class
    /// </summary>
    public class Setup : MvxFormsAndroidSetup<MvxApp, App>
    {
        /// <summary>
        /// Sets the log provider
        /// </summary>
        /// <returns></returns>
        public override MvxLogProviderType GetDefaultLogProviderType()
            => MvxLogProviderType.Console;

        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton<IMvxJsonConverter>(new MvxJsonConverter());
            Mvx.IoCProvider.RegisterSingleton<IDatabaseConnection>(new SQLiteClient());
            base.InitializeFirstChance();
        }
    }
}