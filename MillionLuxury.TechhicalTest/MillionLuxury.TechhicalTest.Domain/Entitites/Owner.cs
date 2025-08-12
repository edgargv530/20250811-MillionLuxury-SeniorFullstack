using MillionLuxury.TechhicalTest.Resources.Validations;

namespace MillionLuxury.TechhicalTest.Domain.Entitites
{
    public class Owner : RootEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        //public byte[] Photo { get; set; } = null!;

        //public DateOnly Birthday { get; set; }

        protected override void CommonValidations()
        {
            //if (Id == Guid.Empty)
            //{
            //    ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeGuidEmpty, nameof(Id)));
            //}

            ValidateRequiredStringFieldAndMaxLength(Name, 100, nameof(Name));
            ValidateRequiredStringFieldAndMaxLength(Address, 200, nameof(Address));

            //if (Photo is null || Photo.Length == 0)
            //{
            //    ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeRequired, nameof(Photo)));
            //}

            //if (Birthday.Year < 1900 || Birthday > DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
            //{
            //    ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeInRange, nameof(Birthday), 1900, DateTime.Now.Year - 18));
            //}
        }
    }
}
