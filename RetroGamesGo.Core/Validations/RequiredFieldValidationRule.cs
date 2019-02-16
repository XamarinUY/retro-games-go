using System;
namespace RetroGamesGo.Core.Validations
{
    public class RequiredFieldValidationRule : IValidationRule<string>
    {
        public string ValidationMessage { get => "Required field"; set => throw new NotImplementedException(); }

        public bool Check(string value)
        {
            return !String.IsNullOrEmpty(value);
        }
    }
}
