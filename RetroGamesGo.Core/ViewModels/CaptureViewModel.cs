namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using System.Windows.Input;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Xamarin.Forms;

    /// <summary>
    /// AR Camera capture logic
    /// </summary>
    public class CaptureViewModel : BaseViewModel
    {
        public ICommand ImageCapturedCommand => new Command<string>(s => Console.WriteLine(s));

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public CaptureViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
