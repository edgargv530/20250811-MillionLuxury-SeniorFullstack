using MillionLuxury.TechhicalTest.ApiRest.Tests.Helpers;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Exceptions;
using MillionLuxury.TechhicalTest.Resources.Validations;

namespace MillionLuxury.TechhicalTest.ApiRest.Tests.UnitTest.Domain
{
    [TestFixture]
    internal class OwnerUnitTest
    {
        [Test]
        [Category(TestHelper.UnitTestValue)]
        public void Owner_Validate_Successful()
        {
            // Arrange
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Address = "123 Main Street",
                //Photo = [3, 8, 4],
                //Birthday = new DateOnly(1990, 5, 10)
            };

            // Act
            owner.Validate();

            // Assert
            Assert.That(true);
        }

        [Test]
        [Category(TestHelper.UnitTestValue)]
        public void Owner_RequiredAttributes_ShouldReturn_ValidationErrorException()
        {
            // Arrange
            var owner = new Owner();
            var validationErrorExceptionExpected = new ValidationErrorException(CommonValidationMessages.ValidationEntityErrors,
            [
                string.Format(CommonValidationMessages.AtributeGuidEmpty, nameof(Owner.Id)),
                string.Format(CommonValidationMessages.AtributeRequired, nameof(Owner.Name)),
                string.Format(CommonValidationMessages.AtributeRequired, nameof(Owner.Address)),
                //string.Format(CommonValidationMessages.AtributeRequired, nameof(Owner.Photo)),
                //string.Format(CommonValidationMessages.AtributeInRange, nameof(Owner.Birthday), 1900, DateTime.Now.Year - 18)
            ]);

            // Act
            var ex = Assert.Throws<ValidationErrorException>(() => owner.Validate());

            // Assert
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Does.Contain(CommonValidationMessages.ValidationEntityErrors));
            ex.ErrorMessages.ForEach(e => Assert.That(validationErrorExceptionExpected.ErrorMessages.Contains(e)));
        }

        [Test]
        [Category(TestHelper.UnitTestValue)]
        public void Owner_StringAttributes_ShouldReturn_ValidationErrorException()
        {
            // Arrange
            var owner = new Owner()
            {
                Id = Guid.NewGuid(),
                Name = "Name".PadRight(101, 'X'),
                Address = "Address".PadRight(201, 'X'),
                //Photo = [3, 8, 4],
                //Birthday = DateOnly.FromDateTime(DateTime.Now)
            };
            var validationErrorExceptionExpected = new ValidationErrorException(CommonValidationMessages.ValidationEntityErrors,
            [
                string.Format(CommonValidationMessages.FieldMaxLength, nameof(Owner.Name), 100),
                string.Format(CommonValidationMessages.FieldMaxLength, nameof(Owner.Address), 200),
                //string.Format(CommonValidationMessages.AtributeInRange, nameof(Owner.Birthday), 1900, DateTime.Now.Year - 18)
            ]);

            // Act
            var ex = Assert.Throws<ValidationErrorException>(() => owner.Validate());

            // Assert
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Does.Contain(CommonValidationMessages.ValidationEntityErrors));
            ex.ErrorMessages.ForEach(e => Assert.That(validationErrorExceptionExpected.ErrorMessages.Contains(e)));
        }
    }
}
