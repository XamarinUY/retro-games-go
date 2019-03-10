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
    using System.Threading.Tasks;

    /// <summary>
    /// AR Camera capture logic
    /// </summary>
    public class CaptureViewModel : BaseViewModel
    {
        public ICommand ImageCapturedCommand => new Command<string>(ImageCaptured, (s) => !IsBusy);

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

            var character = characters.FirstOrDefault(x => x.AssetSticker.Contains(imageName) || x.Name == imageName); //HACK 

            if (character != null && !character.Captured)
            {
                character.Captured = true;
                await characterRepository.UpdateCharacter(character);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await Mvx.IoCProvider.Resolve<IUserDialogs>().AlertAsync($"Capturaste a {imageName}. Ahora puedes utilizar el botón 'Ver personaje' para ubicarlo en el espacio usando realidad aumentada", "Felicitaciones!", "Ok!");
                    await NavigationService.Close(this);
                });
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }



    }
}
