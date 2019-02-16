namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using RetroGamesGo.Core.Models;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public IList<Character> Characters { get; set; }

        private IMvxAsyncCommand captureCommand;

        public IMvxAsyncCommand CaptureCommand => captureCommand ?? (captureCommand = new MvxAsyncCommand(OnCaptureCommand, () => this.IsEnabled));

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            Characters = new ObservableCollection<Character>
            {
                new Character
                {
                    Id = Guid.NewGuid(),
                    Name = "Mario Bros",
                    Number = 1,
                    Year = 1983,
                    Description = "Es un videojuego de arcade desarrollado por Nintendo en el año 1983. Fue creado por Shigeru Miyamoto. Ha sido presentado como un minijuego en la serie de Super Mario Advance y otros juegos. Mario Bros. ha sido relanzado para Wii, Nintendo 3DS y Wii U en los servicios de Consola Virtual en Japón, Norteamérica, Europa y Australia.",
                    Url = "https://es.wikipedia.org/wiki/Mario_Bros.",
                    FunFact = string.Empty,
                    Picture = "marioBros.png",
                    Silhouette = "marioBrosSilhouette.png",
                    Captured = false
                },
                new Character
                {
                    Id = Guid.NewGuid(),
                    Name = "Mario Bros",
                    Number = 2,
                    Year = 1983,
                    Description = "Es un videojuego de arcade desarrollado por Nintendo en el año 1983. Fue creado por Shigeru Miyamoto. Ha sido presentado como un minijuego en la serie de Super Mario Advance y otros juegos. Mario Bros. ha sido relanzado para Wii, Nintendo 3DS y Wii U en los servicios de Consola Virtual en Japón, Norteamérica, Europa y Australia.",
                    Url = "https://es.wikipedia.org/wiki/Mario_Bros.",
                    FunFact = string.Empty,
                    Picture = "marioBros.png",
                    Silhouette = "marioBrosSilhouette.png",
                    Captured = true
                },
            };
        }

        private async Task OnCaptureCommand()
        {
            this.IsBusy = true;
            //await this.NavigationService.Navigate<CaptureViewModel>();
            this.IsBusy = false;

            this.Characters[0].Captured = !this.Characters[0].Captured;
            await this.RaisePropertyChanged(() => this.Characters);
        }
    }
}
