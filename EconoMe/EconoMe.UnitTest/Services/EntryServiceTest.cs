using EconoMe.Models;
using EconoMe.Services.DataAccess;
using EconoMe.Services.Entries;
using EconoMe.UnitTest.Stubs;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EconoMe.UnitTest.Services
{
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
}
