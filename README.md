# EconoMe

## ServiceTest
En LoginPage creamos el siguiente Trigger

``` csharp
    [TestFixture]
    public class EntryServiceTest
    {
        private readonly Mock<IDataBaseService> _databaseService = new Mock<IDataBaseService>();
        
        [Test]
        public async Task Totals_ShouldBeSuccess()
        {
            //Arrange
            var myEntries = EntriesStub.GetMyEntriesWithTwoExpenses();
            var expectedTotal = new Totals()
            {
                Expense = 500,
                Income = 1000
            };

            _databaseService
                .Setup(x => x.GetMyEntries())
                .ReturnsAsync(myEntries);

            var entryService = new EntryService(_databaseService.Object);

            //Act
            var totalsResult = await entryService.GetTotals();

            //Assert
            totalsResult.Should().BeEquivalentTo(expectedTotal);
        }
    }
```

## ValidationTest
En LoginPage creamos el siguiente Trigger

``` csharp
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
```
