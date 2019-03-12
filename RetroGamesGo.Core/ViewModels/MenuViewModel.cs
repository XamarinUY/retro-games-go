namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// Navigation drawer (menu) logic
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {       
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }
    }
}
