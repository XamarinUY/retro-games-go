using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RetroGamesGo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroGameDraw.Core.ViewModels
{
    public class ModalViewModel : MvxNavigationViewModel<Tuple<string,string,string>>
    {
        #region --- Variables ---

        private User winner;

        private IMvxAsyncCommand acceptCommand;

        #endregion


        #region --- Constructor ---
        public ModalViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
        #endregion


        #region --- Properties ---
        /// <summary>
        /// Winner
        /// </summary>
        public User Winner
        {
            get => this.winner;
            set
            {
                if (value == null) return;
                this.winner = value;
                RaisePropertyChanged(() => Winner);
            }
        }

        public bool UserWon { get; set; }
        public bool UserDidntWin { get; set; }


        /// <summary>
        /// Accept 
        /// </summary>
        public IMvxAsyncCommand AcceptCommand
        {
            get
            {
                return acceptCommand ?? (acceptCommand = new MvxAsyncCommand(async () =>
                {
                    await NavigationService.Close(this);
                }));
            }
        }
        #endregion


        #region --- Overrides ---
        public override void Prepare(Tuple<string, string, string> user)
        {
            this.UserDidntWin = true;
            if (user.Item1 != null)
            { 
                this.UserWon = true;
                this.UserDidntWin = false;
                this.Winner = new User { Name = user.Item1, Document = user.Item2, Country = user.Item3 };
            }
            RaisePropertyChanged(() => UserWon);
            RaisePropertyChanged(() => UserDidntWin);
        }

        #endregion
    }
}
