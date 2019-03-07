namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using RetroGamesGo.Core.Repositories;
    using Xamarin.Forms;

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

                //var view = MainActivity.Instance.FindViewById(Android.Resource.Id.Content);
                //var snackBar = Snackbar.Make(view, $"Capturaste a {((AugmentedImage)image).Name}", Snackbar.LengthIndefinite);
                //snackBar.SetDuration(5000);
                //snackBar.Show();
            }
        }

    }
}
