using MillionLuxury.TechhicalTest.Resources.Validations;

namespace MillionLuxury.TechhicalTest.Domain.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public List<string> ErrorMessages { get; private set; } = [];

        public ValidationErrorException(string error) : base(CommonValidationMessages.ValidationExceptionMessage)
        {
            ErrorMessages.Add(error);
        }

        public ValidationErrorException(string error, Exception innerException) : base(CommonValidationMessages.ValidationExceptionMessage, innerException)
        {
            ErrorMessages.Add(error);
        }

        public ValidationErrorException(string message, string error) : base(message)
        {
            ErrorMessages.Add(error);
        }

        public ValidationErrorException(string message, string error, Exception innerException) : base(message, innerException)
        {
            ErrorMessages.Add(error);
        }

        public ValidationErrorException(string message, IEnumerable<string> errors) : base(message)
        {
            ErrorMessages.AddRange(errors);
        }

        public ValidationErrorException(string message, IEnumerable<string> errors, Exception innerException) : base(message, innerException)
        {
            ErrorMessages.AddRange(errors);
        }
    }
}
