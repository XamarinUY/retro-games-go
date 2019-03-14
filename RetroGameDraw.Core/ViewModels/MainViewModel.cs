namespace RetroGameDraw.Core.ViewModels
{
    #region --- Usings---

    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using MvvmCross;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using RetroGamesGo.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections.ObjectModel;


    #endregion

    /// <summary>
    /// Main ViewModels
    /// </summary>
    public class MainViewModel : MvxNavigationViewModel
    {
        #region --- Variables ---

        private bool isBusy;
        private List<User> allUsers;
        private IMvxAsyncCommand refreshUsersCommand;
        private IMvxAsyncCommand getWinnerUserCommand;

        #endregion


        #region --- Properties --- 

        /// <summary>
        /// Get's if the viewModel is busy doing something
        /// </summary>
        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                this.isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// Database service
        /// </summary>
        public IDataService DataService
        {
            get;
        }


        public ObservableCollection<User> Users { get; set; }


        public IMvxAsyncCommand RefreshUsersCommand
        {
            get
            {
                return refreshUsersCommand ?? (refreshUsersCommand = new MvxAsyncCommand(async () =>
                {
                    await LoadDataAsync();
                }));
            }
        }

        public IMvxAsyncCommand GetWinnerUserCommand
        {
            get
            {
                return getWinnerUserCommand ?? (getWinnerUserCommand = new MvxAsyncCommand(async () =>
                {
                    this.IsBusy = true;
                    try
                    {
                        var winner = await this.DataService.GetWinnerUserAsync();
                        var tuple = new Tuple<string, string, string>(winner.Name, winner.Document, winner.Country);
                        await NavigationService.Navigate<ModalViewModel, Tuple<string, string, string>>(tuple);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        this.IsBusy = false;
                    }
                }));
            }
        }
        #endregion

        #region --- Constructor ---


        /// <summary>
        /// Constructor
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.DataService = Mvx.IoCProvider.GetSingleton<IDataService>();
        }

        #endregion

        public override Task Initialize()
        {
            return Task.Run(async () =>
            {
                await LoadDataAsync();
            });
        }



        #region --- LoadData ---

        /// <summary>
        /// Reloads the data
        /// </summary>
        private async Task LoadDataAsync()
        {
            this.IsBusy = true;
            try
            {
                this.allUsers = await this.DataService.GetUsersAsync();

                this.Users = new ObservableCollection<User>(allUsers.Select(item => (item)));

                await this.RaisePropertyChanged(() => this.Users);
            }
            catch (Exception ex)
            {
                //TODO
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
