namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Commands;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private IMvxCommand showArCameraCommand;


        /// <summary>
        /// Opens the Camera for AR 
        /// </summary>
        public IMvxCommand ShowArCameraCommand => showArCameraCommand ?? (showArCameraCommand =
                                                      new MvxCommand(async () => { await this.NavigationService.Navigate<ArCameraViewModel>(); }));
        
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
