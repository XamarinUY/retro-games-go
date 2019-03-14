namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Models;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Acr.UserDialogs;
    using MvvmCross;
    using System.Linq;
    using MvvmCross.Commands;
    using Repositories;
    using MvvmCross.Plugin.Messenger;
    using Messages;
    using System.Threading.Tasks;
    using System;


    /// <summary>
    /// Places a character
    /// </summary>
    public class PlaceCharacterViewModel : BaseViewModel<Character>
    {
        private List<Character> characters;
        private readonly ICharacterRepository characterRepository;
        private readonly IMvxMessenger messengerService;

        /// <summary>
        /// Shows a sheet to select the character to place
        /// </summary>
        public ICommand SelectCharacterCommand => new MvxCommand(SelectCharacter);


        /// <summary>
        /// 3D Character to place
        /// </summary>
        public Character Character
        {
            get; set;
        }


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public PlaceCharacterViewModel(IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService, ICharacterRepository characterRepository) : base(logProvider, navigationService)
        {
            this.characterRepository = characterRepository;
            this.messengerService =  Mvx.IoCProvider.GetSingleton<IMvxMessenger>();
        }


        /// <summary>
        /// Saves the parameter
        /// </summary>        
        public override async void Prepare(Character parameter)
        {
            this.Character = parameter;
        }


        /// <summary>
        /// Select character function
        /// </summary>
        private void SelectCharacter()
        {
            InvokeOnMainThread(async () =>
            {
                try
                {
                    var userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();
                    userDialog.ActionSheet(new ActionSheetConfig
                    {
                        Title = "Seleccione el personaje",
                        Options = this.characters.Select(x => new ActionSheetOption(x.Name, () =>
                         {
                             this.messengerService.Publish(new SelectedCharacterMessage(x, this));
                         })).ToArray()
                    });
                }

                catch (Exception ex)
                {
                    throw;
                }
            });       
        }

        public override Task Initialize()
        {
            return Task.Run(async () => {
                this.characters = await characterRepository.GetAll();
                SelectCharacter(); 
            });
        }
    }
}
