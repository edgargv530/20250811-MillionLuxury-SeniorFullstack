using MillionLuxury.TechhicalTest.Domain.Exceptions;
using MillionLuxury.TechhicalTest.Resources.Validations;

namespace MillionLuxury.TechhicalTest.Domain.Entitites
{
    public abstract class RootEntity
    {
        protected virtual List<string> ErrorMessages { get; private set; } = [];

        public virtual void Validate()
        {
            CommonValidations();
            ThrowErrorMessages();
        }

        protected abstract void CommonValidations();

        protected virtual void ThrowErrorMessages()
        {
            if (ErrorMessages != null && ErrorMessages.Any())
            {
                throw new ValidationErrorException(CommonValidationMessages.ValidationEntityErrors, ErrorMessages);
            }
        }

        protected virtual void ValidateRequiredStringFieldAndMaxLength(string value, int maxLength, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeRequired, propertyName));
            }
            else if (value.Length > maxLength)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.FieldMaxLength, propertyName, maxLength));
            }
        }
    }
}
