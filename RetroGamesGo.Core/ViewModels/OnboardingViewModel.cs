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
        private MvxCommand play;
        public bool ShowLottie { get; set; } = true;
        public bool ShowOnboard { get; set; } = false;


        public OnboardingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public async override void Prepare()
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

        public ICommand Play => this.play ?? (
               this.play = new MvxCommand(async () =>
               {
                   Settings.OnboardingShown = true;
                   await NavigationService.Navigate<MainViewModel>();
               }));
    }
}
