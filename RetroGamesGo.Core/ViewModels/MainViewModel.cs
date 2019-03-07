﻿namespace RetroGamesGo.Core.ViewModels
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
    using System.Linq;
    using Acr.UserDialogs;
    using RetroGamesGo.Core.Helpers;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {

        private readonly ICharacterRepository characterRepository;

        public IList<Character> Characters { get; set; }

        private IMvxAsyncCommand captureCommand;

        public IMvxAsyncCommand CaptureCommand => captureCommand ?? (captureCommand = new MvxAsyncCommand(OnCaptureCommand, () => this.IsEnabled));

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.characterRepository = Mvx.IoCProvider.Resolve<ICharacterRepository>();
        }

        public override async Task Initialize()
        {
            await LoadCharacters();
            Characters.ToList().ForEach(c => c.Captured = true);
            var allCaptured = Characters.All(c => c.Captured);

            if(!Settings.FormCompleted && allCaptured)
            {
                var goToChallengeCompleted = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync("Has desbloqueado todos los personajes de Retro Games GO! Completa el formulario para participar del sorteo :)", "Felicitaciones!", "Entendido");
                if(goToChallengeCompleted)
                    GoToChallengeCompletedPage();
            }
        }

        private Task LoadCharacters()
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
            await this.NavigationService.Navigate<CaptureViewModel>();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            LoadCharacters();
        }
    }
}
