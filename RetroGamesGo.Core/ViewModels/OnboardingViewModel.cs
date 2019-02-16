namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using RetroGamesGo.Core.Helpers;
    using RetroGamesGo.Core.Models;

    public class OnboardingViewModel : BaseViewModel
    {
        public IList<string> InfoImages { get; set; }
        private MvxCommand play;
        public bool ShowLottie { get; set; } = true;
        public bool ShowOnboard { get; set; } = false;


        public OnboardingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }

        public async override void ViewAppeared()
        {
            await Load();
            base.ViewAppeared();
        }

        public async Task Load()
        {
            await Task.Delay(5000);
            ShowLottie = false;
            ShowOnboard = true;
            await RaisePropertyChanged(() => ShowLottie);
            await RaisePropertyChanged(() => ShowOnboard);
        }

        public override void Prepare()
        {
            InfoImages = new ObservableCollection<string>()
                {
                    "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png",
                    "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png",
                    "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png"
                };
            RaisePropertyChanged(() => InfoImages);
        }

        public ICommand Play => this.play ?? (
               this.play = new MvxCommand(async () =>
               {
                   Settings.OnboardingShown = true;
                   await NavigationService.Close(this);
                   await NavigationService.Navigate<MainViewModel>();
               }));
    }
}
