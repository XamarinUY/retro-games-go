using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using RetroGamesGo.Core.Models;
using RetroGamesGo.Core.Validations;
using Xamarin.Forms;

namespace RetroGamesGo.Core.ViewModels
{
    public class ChallengeCompletedViewModel : BaseViewModel, INotifyPropertyChanged
    {
        // Attributes
        private User User { get; set; }
        public new event PropertyChangedEventHandler PropertyChanged;

        // Properties
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LastName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PhoneNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Document { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Country { get; set; } = new ValidatableObject<string>();

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
        public ChallengeCompletedViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            Name.Validations.Add(new RequiredFieldValidationRule());
            LastName.Validations.Add(new RequiredFieldValidationRule());
            Email.Validations.Add(new EmailValidationRule());
            Email.Validations.Add(new RequiredFieldValidationRule());
            PhoneNumber.Validations.Add(new RequiredFieldValidationRule());
            Document.Validations.Add(new RequiredFieldValidationRule());
            Country.Validations.Add(new RequiredFieldValidationRule());
        }

        private async Task RegisterUser()
        {
            if (!Validate())
                return;

            User = new User
            {
                Name = Name.Value,
                LastName = LastName.Value,
                Email = Email.Value,
                PhoneNumber = PhoneNumber.Value,
                Document = Document.Value,
                Country = Country.Value
            };

            // TODO: Call api
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
