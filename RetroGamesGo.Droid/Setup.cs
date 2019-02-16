namespace RetroGamesGo.Droid
{
    using Core;
    using MvvmCross.Forms.Platforms.Android.Core;
    using MvvmCross.Logging;
    

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

        /// <summary>
        /// Initializes the platform services
        /// </summary>
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();            
        }
    }
}