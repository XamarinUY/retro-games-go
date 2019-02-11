namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
