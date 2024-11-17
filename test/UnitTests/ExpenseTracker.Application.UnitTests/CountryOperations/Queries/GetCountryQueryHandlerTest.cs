using Moq;
using Xunit;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Application.CountryOperations.Queries;
using ErrorOr;
using System.Threading;
using System.Threading.Tasks;
namespace ExpenseTracker.Application.UnitTests.CountryOperations.Queries;

public class GetCountryQueryHandlerTests
{
    [Fact]
    public async Task HandleGetCountryQuery_WhenCountryExists_ShouldReturnCountry()
    {
        // Arrange
        var countryId = Guid.NewGuid();
        var expectedCountry = new GetCountryResult(countryId, "TestCountry");

        var mockCountryService = new Mock<ICountryService>();
        mockCountryService
            .Setup(service => service.GetCountryByIdAsync(countryId))
            .ReturnsAsync(expectedCountry);

        var handler = new GetCountryQueryHandler(mockCountryService.Object);
        var query = new GetCountryQuery(countryId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError == false, "Result should not contain an error.");
        Assert.Equal(expectedCountry.Id, result.Value.Id);
        Assert.Equal(expectedCountry.Name, result.Value.Name);

        mockCountryService.Verify(
            service => service.GetCountryByIdAsync(countryId),
            Times.Once,
            "GetCountryByIdAsync should be called once with the correct country ID.");
    }

    [Fact]
    public async Task HandleGetCountryQuery_WhenCountryDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var countryId = new Guid();
        var expectedError = Error.NotFound("Country.NotFound", "NotFound");

        var mockCountryService = new Mock<ICountryService>();
        mockCountryService
            .Setup(service => service.GetCountryByIdAsync(countryId))
            .ReturnsAsync(expectedError);

        var handler = new GetCountryQueryHandler(mockCountryService.Object);
        var query = new GetCountryQuery(countryId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "Result should contain an error.");
        Assert.Equal(expectedError, result.FirstError);

        mockCountryService.Verify(
            service => service.GetCountryByIdAsync(countryId),
            Times.Once,
            "GetCountryByIdAsync should be called once with the correct country ID.");
    }

}
