namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// AR Camera capture logic
    /// </summary>
    public class CaptureViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public CaptureViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
