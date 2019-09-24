using EconoMe.Validations;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.UnitTest.Validations
{
    [TestFixture]
    public class IsNotNullOrEmptyTest
    {
        [Test]
        public void NotEmptyValue_ShouldBeSuccess()
        {
            //Arrange
            var textProperty = new ValidatableObject<string>
            {
                Value = "Hello World"
            };

            textProperty.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Property should not be empty"
            });

            //act
            var isValidProperty = textProperty.Validate();

            //Assert
            isValidProperty.Should().BeTrue();
        }

        [Test]
        public void EmptyValue_ShouldBeInvalid()
        {
            //Arrange
            var textProperty = new ValidatableObject<string>
            {
                Value = string.Empty
            };

            textProperty.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Property should not be empty"
            });

            //act
            var isValidProperty = textProperty.Validate();

            //Assert
            isValidProperty.Should().BeFalse();
        }
    }
}
