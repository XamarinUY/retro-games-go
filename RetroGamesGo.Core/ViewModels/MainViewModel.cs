namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using MvvmCross;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Xamarin.Forms;
    using RetroGamesGo.Core.Repositories;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        // Properties
        public Command GoToChallengeCompletedPageCommand => new Command(GoToChallengeCompletedPage);

        // Attributes
        private readonly IMvxNavigationService navigationService;

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.navigationService = navigationService;

            // DATABASE - Testing
            //TestCharacterRepository();
        }

        private void GoToChallengeCompletedPage()
        {
            navigationService.Navigate<ChallengeCompletedViewModel>();
        }

        private void TestCharacterRepository()
        {
            Task.Run(async () =>
            {
                ICharacterRepository characterRepo = Mvx.IoCProvider.Resolve<ICharacterRepository>();

                // Reset characters table
                await characterRepo.DeleteAll();

                //Adding characters
                await characterRepo.AddCharacter(new Models.Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Super Mario Bros.",
                    Description = "Description 1",
                    FunFact = "fun",
                    Picture = "mario.png",
                    Silhouette = "mario_silhouette.png",
                    Url = "http:lala.mario.com"
                });

                var donkeyGuid = Guid.NewGuid();
                await characterRepo.AddCharacter(new Models.Character()
                {
                    Id = donkeyGuid,
                    Name = "Donkey Kong",
                    Description = "Description 2",
                    FunFact = "fun",
                    Picture = "donkey.png",
                    Silhouette = "donkey_silhouette.png",
                    Url = "http:lala.donkey.com"
                });

                await characterRepo.AddCharacter(new Models.Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sonic",
                    Description = "Description 3",
                    FunFact = "fun",
                    Picture = "Sonic.png",
                    Silhouette = "Sonic_silhouette.png",
                    Url = "http:lala.Sonic.com"
                });

                // Get All Test
                var all = await characterRepo.GetAll();

                // Get Donkey Kong character
                var donkeyCharacter = await characterRepo.GetCharacter(donkeyGuid);

                // Get Donkey Kong character
                donkeyCharacter.Captured = true;
                donkeyCharacter.Description = "Description updated";

                // Update Donkey Kong character
                await characterRepo.UpdateCharacter(donkeyCharacter);

                // Get update Donkey Kong character
                var updateDonkeyCharacter = await characterRepo.GetCharacter(donkeyGuid);
            });
        }
    }
}
