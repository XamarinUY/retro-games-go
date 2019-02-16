namespace RetroGamesGo.Core.ViewModels
{
    using System;
    using MvvmCross;
    using MvvmCross.Localization;
    using MvvmCross.ViewModels;
    using System.Threading.Tasks;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.JsonLocalization;
    using Services;
    using System.ComponentModel;

    /// <summary>
    /// Base class for all our ViewModels
    /// </summary>
    public abstract class BaseViewModel : MvxNavigationViewModel //, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        private bool isBusy;
        private readonly IMvxTextProviderBuilder textProviderBuilder;


        /// <summary>
        /// Page title 
        /// </summary>
        //public string Title { get; set; }

        //public bool IsBusy { get; set; }
        //public bool IsEnabled
        //{
        //    get
        //    {
        //        return !IsBusy;
        //    }
        //}

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
                RaisePropertyChanged(() => IsEnabled);
            }
        }


        /// <summary>
        /// Gets if the ViewModel is enabled.
        /// It's the inverse of IsBusy for easier binding. (If IsBusy = true them IsEnabled = false)
        /// </summary>
        public bool IsEnabled
        {
            get => !this.isBusy;
            set
            {
                this.isBusy = !value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }



        /// <summary>
        /// Source for localized texts
        /// </summary>
        public IMvxLanguageBinder TextSource =>
            new MvxLanguageBinder(TextProviderConstants.GeneralNamespace, TextProviderConstants.ClassName);


        /// <summary>
        /// Helper method for getting a localized text
        /// </summary>
        /// <param name="text">Text to get</param>
        /// <returns>Localized text</returns>
        public string GetText(string text)
        {
            return this.textProviderBuilder.TextProvider.GetText(
                TextProviderConstants.GeneralNamespace, TextProviderConstants.ClassName, text);
        }


        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.IsBusy = false;
            this.textProviderBuilder = Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>();
        }


        public Task LogExceptionAsync(Exception ex)
        {
            return Task.FromResult(true);
        }
    }


    /// <summary>
    /// Base ViewModel with parameters
    /// </summary>
    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter> where TParameter : class
    {
        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Called with the ViewModel's parameters
        /// </summary>
        public abstract void Prepare(TParameter parameter);
    }


    /// <summary>
    /// Base ViewModel with parameters and result
    /// </summary>
    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModel, IMvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Called with the ViewModel's parameters
        /// </summary>
        public abstract void Prepare(TParameter parameter);

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }


    /// <summary>
    /// Base ViewModel with parameters and result
    /// </summary>
    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
        where TResult : class
    {
        protected BaseViewModelResult(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }

    }
}
