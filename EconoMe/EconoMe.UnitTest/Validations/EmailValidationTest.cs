using EconoMe.Validations;
using FluentAssertions;
using NUnit.Framework;

namespace EconoMe.UnitTest.Validations
{
    [TestFixture]
    public class EmailValidationTest
    {
        [TestCase("alex.llanes@gmail.com")]
        [TestCase("alex@hotmail.com")]
        public void EmailValue_ShouldBeSuccess(string email)
        {
            //Arrange
            var textProperty = new ValidatableObject<string>
            {
                Value = email
            };

            textProperty.Validations.Add(new EmailRule()
            {
                ValidationMessage = "Invalid format"
            });

            //act
            var isValidProperty = textProperty.Validate();

            //Assert
            isValidProperty.Should().BeTrue();
        }

        [TestCase("alex.llanesgmail.com")]
        [TestCase("@hotmail.com")]
        public void EmailValue_ShouldBeInvalid(string email)
        {
            //Arrange
            var textProperty = new ValidatableObject<string>
            {
                Value = email
            };

            textProperty.Validations.Add(new EmailRule()
            {
                ValidationMessage = "Invalid format"
            });

            //act
            var isValidProperty = textProperty.Validate();

            //Assert
            isValidProperty.Should().BeFalse();
        }
    }
}
