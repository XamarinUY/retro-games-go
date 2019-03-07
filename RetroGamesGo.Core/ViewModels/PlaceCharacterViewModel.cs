namespace RetroGamesGo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Models;
    
    /// <summary>
    /// Places a character
    /// </summary>
    public class PlaceCharacterViewModel : BaseViewModel<Character>
    {
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
        public PlaceCharacterViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }


        /// <summary>
        /// Saves the parameter
        /// </summary>        
        public override void Prepare(Character parameter)
        {
            this.Character = parameter;
        }
     
    }
}
