namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using MvvmCross;
    using System.Collections.Generic;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Xamarin.Forms;
    using RetroGamesGo.Core.Repositories;
    using RetroGamesGo.Core.Models;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public IList<Character> Characters { get; set; }

        private IMvxAsyncCommand captureCommand;

        public IMvxAsyncCommand CaptureCommand => captureCommand ?? (captureCommand = new MvxAsyncCommand(OnCaptureCommand, () => this.IsEnabled));

        // Properties
        public Command GoToChallengeCompletedPageCommand => new Command(GoToChallengeCompletedPage);

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private void GoToChallengeCompletedPage()
        {
            this.NavigationService.Navigate<ChallengeCompletedViewModel>();
        }

        private async Task OnCaptureCommand()
        {
            this.IsBusy = true;
            //await this.NavigationService.Navigate<CaptureViewModel>();
            this.IsBusy = false;
        }
    }
}
