namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;


    /// <summary>
    /// Root empty ViewModel for setup up the startup.
    /// </summary>
    public class RootViewModel : MvxViewModel
    {
        private bool viewAppeared;
        private readonly IMvxNavigationService navigationService;


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public RootViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }


        /// <summary>
        /// On appearing loads the login or the menu and main viewmodel
        /// </summary>
        public override void ViewAppeared()
        {
            if (viewAppeared) return;
            MvxNotifyTask.Create(async () =>
            {
                try
                {
                    // Loads the menu and first viewModel
                    this.viewAppeared = true;           
                    await this.navigationService.Navigate<MenuViewModel>();
                    await this.navigationService.Navigate<MainViewModel>();
                }
                catch
                {
                    // This shouldn't fail
                }
            });
        }


    }
}
