namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Xamarin.Forms;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        // Properties
        public Command GoToChallengeCompletedPageCommand => new Command(GoToChallengeCompletedPage);

        // Attributes
        private readonly IMvxNavigationService navigationService;

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.navigationService = navigationService;
        }

        private void GoToChallengeCompletedPage()
        {
            navigationService.Navigate<ChallengeCompletedViewModel>();
        }
    }
}
