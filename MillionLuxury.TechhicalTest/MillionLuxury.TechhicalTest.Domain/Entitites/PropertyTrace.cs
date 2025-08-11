using MillionLuxury.TechhicalTest.Resources.Validations;
using System.Globalization;

namespace MillionLuxury.TechhicalTest.Domain.Entitites
{
    public class PropertyTrace : RootEntity
    {
        public Guid Id { get; set; }

        public DateTime DateSale { get; set; }

        public string Name { get; set; } = null!;

        public decimal Value { get; set; }

        public decimal Tax { get; set; }

        protected override void CommonValidations()
        {
            if (Id == Guid.Empty)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeGuidEmpty, nameof(Id)));
            }

            if (DateSale > DateTime.Now)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.DateLessThanToday, nameof(DateSale)));
            }

            ValidateRequiredStringFieldAndMaxLength(Name, 100, nameof(Name));

            if (Value <= 0)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeEqualOrGreaterThanZero, nameof(Value)));
            }

            if (Tax < 0 || Tax > 100)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeInRange, nameof(Tax), 0, 100));
            }
        }
    }
}
