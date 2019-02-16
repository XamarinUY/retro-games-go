namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// AR Camera capture logic
    /// </summary>
    public class ArCameraViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public ArCameraViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
