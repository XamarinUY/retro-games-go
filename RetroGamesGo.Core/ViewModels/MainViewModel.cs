namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using RetroGamesGo.Core.Models;

    /// <summary>
    /// Main view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public IList<Character> Characters { get; set; }

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
                    Name = string.Empty,
                    Image = "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png",
                    FunFact = string.Empty,
                    Unlocked = true
                },
                new Character
                {
                    Id = Guid.NewGuid(),
                    Name = string.Empty,
                    Image = "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png",
                    FunFact = string.Empty,
                    Unlocked = false
                },
                new Character
                {
                    Id = Guid.NewGuid(),
                    Name = string.Empty,
                    Image = "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png",
                    FunFact = string.Empty,
                    Unlocked = false
                },
                new Character
                {
                    Id = Guid.NewGuid(),
                    Name = string.Empty,
                    Image = "https://i.pinimg.com/originals/f6/32/5f/f6325fa86a0a2915d3545dc39d359e2f.png",
                    FunFact = string.Empty,
                    Unlocked = false
                }
            };
        }
    }
}
