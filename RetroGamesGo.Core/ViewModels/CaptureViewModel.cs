namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using RetroGamesGo.Core.Repositories;
    using Xamarin.Forms;
    using Acr.UserDialogs;
    using MvvmCross;

    /// <summary>
    /// AR Camera capture logic
    /// </summary>
    public class CaptureViewModel : BaseViewModel
    {
        public ICommand ImageCapturedCommand => new Command<string>(ImageCaptured);

        private readonly ICharacterRepository characterRepository;

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public CaptureViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ICharacterRepository characterRepository) : base(logProvider, navigationService)
        {
            this.characterRepository = characterRepository;
        }

        private async void ImageCaptured(string imageName)
        {
            var characters = await characterRepository.GetAll();
            var character = characters.FirstOrDefault(x => x.Name == imageName);
            if (character != null && !character.Captured)
            {
                character.Captured = true;
                await characterRepository.UpdateCharacter(character);

                await Mvx.IoCProvider.Resolve<IUserDialogs>().AlertAsync($"Capturaste a {imageName}. Puedes utilizar el botón 'Ver personaje' para ubicarlo en el espacio usando realidad aumentada", "Ok!");
                await NavigationService.Close(this);
            }
        }

    }
}
