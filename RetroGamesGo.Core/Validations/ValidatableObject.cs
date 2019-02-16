using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RetroGamesGo.Core.Validations
{
    public class ValidatableObject<T> : INotifyPropertyChanged, IValidity
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly List<IValidationRule<T>> validations;
        public List<IValidationRule<T>> Validations => validations;

        public List<string> Errors { get; set; }
        public T Value { get; set; }
        public bool IsValid { get; set; }

        public ValidatableObject()
        {
            IsValid = true;
            Errors = new List<string>();
            validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = validations.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }

}
