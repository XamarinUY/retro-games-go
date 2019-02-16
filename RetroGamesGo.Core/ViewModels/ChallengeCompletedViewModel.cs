using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using RetroGamesGo.Core.Models;
using RetroGamesGo.Core.Services;
using RetroGamesGo.Core.Utils;
using RetroGamesGo.Core.Validations;
using RetroGamesGo.Models;
using Xamarin.Forms;

namespace RetroGamesGo.Core.ViewModels
{
    public class ChallengeCompletedViewModel : BaseViewModel
    {
        // Attributes
        private User User { get; set; }
        private IRequestProvider RequestProvider { get; set; }
        private bool isLoading;

        // Properties
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LastName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PhoneNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Document { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Country { get; set; } = new ValidatableObject<string>();

        // IsBusy not working
        public bool IsLoading 
        {
            get => isLoading;
            set 
            { 
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
                RaisePropertyChanged(() => IsNotLoading); 
            } 
        }

        public bool IsNotLoading { get { return !IsLoading; }}

        // Fields validation commands
        public ICommand ValidateNameCommand => new Command(() => Name.Validate());
        public ICommand ValidateLastNameCommand => new Command(() => LastName.Validate());
        public ICommand ValidateEmailCommand => new Command(() => Email.Validate());
        public ICommand ValidatePhoneNumberCommand => new Command(() => PhoneNumber.Validate());
        public ICommand ValidateDocumentCommand => new Command(() => Document.Validate());
        public ICommand ValidateCountryCommand => new Command(() => Country.Validate());

        public ICommand RegisterUserCommand => new Command(async ()=> await RegisterUser());

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public ChallengeCompletedViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IRequestProvider requestProvider) : base(logProvider, navigationService)
        {
            Name.Validations.Add(new RequiredFieldValidationRule());
            LastName.Validations.Add(new RequiredFieldValidationRule());
            Email.Validations.Add(new EmailValidationRule());
            Email.Validations.Add(new RequiredFieldValidationRule());
            PhoneNumber.Validations.Add(new RequiredFieldValidationRule());
            Document.Validations.Add(new RequiredFieldValidationRule());
            Country.Validations.Add(new RequiredFieldValidationRule());

            RequestProvider = requestProvider;
        }

        private async Task RegisterUser()
        {
            if (!Validate())
                return;

            IsLoading = true;

            User = new User
            {
                Name = Name.Value + " " + LastName.Value,
                Email = Email.Value,
                CellPhone = PhoneNumber.Value,
                Document = Document.Value,
                Country = Country.Value
            };

            // Save the user
            var result = await RequestProvider.PostAsync<User>(Constants.RequestProvider.PostUserEndpoint, User);

            IsLoading = false;
        }

        public bool Validate()
        {
            Name.Validate();
            LastName.Validate();
            Email.Validate();
            PhoneNumber.Validate();
            Document.Validate();
            Country.Validate();

            return Name.IsValid && LastName.IsValid && Email.IsValid && PhoneNumber.IsValid && Document.IsValid && Country.IsValid;
        }
    }
}
