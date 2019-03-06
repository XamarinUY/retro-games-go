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

        private readonly ICharacterRepository characterRepository;

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
            this.characterRepository = Mvx.IoCProvider.Resolve<ICharacterRepository>();
        }

        public override Task Initialize()
        {
            return Task.Run(async () => 
            {
                this.Characters = await this.characterRepository.GetAll();
                await this.RaisePropertyChanged(() => this.Characters);
            });
        }


        /// <summary>
        /// Go the challenge completed page
        /// </summary>
        private void GoToChallengeCompletedPage()
        {
            this.NavigationService.Navigate<ChallengeCompletedViewModel>();
        }


        /// <summary>
        /// Open the camera an try to capture models
        /// </summary>
        /// <returns></returns>
        private async Task OnCaptureCommand()
        {
            //await this.NavigationService.Navigate<CaptureViewModel>();
            await this.NavigationService.Navigate<PlaceCharacterViewModel, Character>(this.Characters[0]);
            
        }
    }
}
