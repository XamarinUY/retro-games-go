using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetroGameDraw.Core.ViewModels
{
    public class RootViewModel : MvxNavigationViewModel
    {

        public IMvxAsyncCommand ShowChildCommand { get; }

        public RootViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxViewModelLoader mvxViewModelLoader) : base(logProvider, navigationService)
        {
            ShowChildCommand = new MvxAsyncCommand(async () =>
            {
                var result = await NavigationService.Navigate<MainViewModel>();
            });

        }

        public override async Task Initialize()
        {

            await base.Initialize();

        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();
        }
    }
}
