using MillionLuxury.TechhicalTest.Resources.Validations;
using System.Xml.Linq;

namespace MillionLuxury.TechhicalTest.Domain.Entitites
{
    public class PropertyImage : RootEntity
    {
        public Guid Id { get; set; }

        public byte[] File { get; set; } = null!;

        public bool Enabled { get; set; }

        protected override void CommonValidations()
        {
            if (Id == Guid.Empty)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeGuidEmpty, nameof(Id)));
            }

            if (File is null || File.Length == 0)
            {
                ErrorMessages.Add(string.Format(CommonValidationMessages.AtributeRequired, nameof(File)));
            }
        }
    }
}
