using MillionLuxury.TechhicalTest.Resources.Validations;

namespace MillionLuxury.TechhicalTest.Domain.Entitites
{
    public class Property : RootEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string InternalCode { get; set; } = null!;

        public int Year { get; set; }

        public Owner Owner { get; set; } = null!;

        public IEnumerable<PropertyImage> Images { get; set; } = null!;

        public IEnumerable<PropertyTrace> Traces { get; set; } = null!;

        protected override void CommonValidations()
        {
            if (Id == Guid.Empty)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeGuidEmpty, nameof(Id)));
            }

            ValidateRequiredStringFieldAndMaxLength(Name, 100, nameof(Name));
            ValidateRequiredStringFieldAndMaxLength(Address, 200, nameof(Address));

            if (Price <= 0)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeEqualOrGreaterThanZero, nameof(Price)));
            }

            ValidateRequiredStringFieldAndMaxLength(InternalCode, 50, nameof(InternalCode));

            if (Year < 1900 || Year > DateTime.Now.Year)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeInRange, nameof(Year), 1900, DateTime.Now.Year));
            }

            if (Owner is null)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeRequired, nameof(Owner)));
            }

            if (Images is null || !Images.Any())
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeRequired, nameof(Images)));
            }
        }
    }
}
