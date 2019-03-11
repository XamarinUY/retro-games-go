namespace RetroGamesGo.Core.ViewModels
{    
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Helpers;
 
    /// <summary>
    /// Onboardirng logic showing who to playe
    /// </summary>
    public class OnboardingViewModel : BaseViewModel
    {
        private MvxAsyncCommand play;

        public bool ShowLottie { get; set; } = true;

        public bool ShowOnboard { get; set; } = false;


        public OnboardingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override async void Prepare()
        {
            if (Settings.OnboardingShown)
            {
                ShowLottie = false;
                ShowOnboard = true;
                await RaisePropertyChanged(() => ShowLottie);
                await RaisePropertyChanged(() => ShowOnboard);
            }
            else
            {
                await Load();
            }
        }

        public async Task Load()
        {
            await Task.Delay(5000);
            ShowLottie = false;
            ShowOnboard = true;
            await RaisePropertyChanged(() => ShowLottie);
            await RaisePropertyChanged(() => ShowOnboard);
        }       

        public IMvxAsyncCommand Play => this.play ?? (
               this.play = new MvxAsyncCommand(async () =>
               {
                   if (!Settings.OnboardingShown)
                   {
                       Settings.OnboardingShown = true;
                       await NavigationService.Navigate<MainViewModel>();
                   }
                   else
                   {
                       await NavigationService.Close(this);
                   }
               }));
    }
}
