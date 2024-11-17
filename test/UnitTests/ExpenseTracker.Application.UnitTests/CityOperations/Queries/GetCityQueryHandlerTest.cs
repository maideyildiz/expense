using Moq;
using Xunit;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ErrorOr;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.CityOperations.Queries;
namespace ExpenseTracker.Application.UnitTests.CityOperations.Queries;

public class GetCityQueryHandlerTest
{
    [Fact]
    public async Task HandleGetCityQuery_WhenCountryExists_ShouldReturnCity()
    {
        // Arrange
        var cityId = Guid.NewGuid();
        var expectedCity = new GetCityResult(cityId, "TestCity");

        var mockCityService = new Mock<ICityService>();
        mockCityService
            .Setup(service => service.GetCityByIdAsync(cityId))
            .ReturnsAsync(expectedCity);

        var handler = new GetCityQueryHandler(mockCityService.Object);
        var query = new GetCityQuery(cityId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError == false, "Result should not contain an error.");
        Assert.Equal(expectedCity.Id, result.Value.Id);
        Assert.Equal(expectedCity.Name, result.Value.Name);

        mockCityService.Verify(
            service => service.GetCityByIdAsync(cityId),
            Times.Once,
            "GetCityByIdAsync should be called once with the correct city ID.");
    }

    [Fact]
    public async Task HandleGetCityQuery_WhenCityDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var cityId = new Guid();
        var expectedError = Error.NotFound("City.NotFound", "NotFound");

        var mockCityService = new Mock<ICityService>();
        mockCityService
            .Setup(service => service.GetCityByIdAsync(cityId))
            .ReturnsAsync(expectedError);

        var handler = new GetCityQueryHandler(mockCityService.Object);
        var query = new GetCityQuery(cityId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "Result should contain an error.");
        Assert.Equal(expectedError, result.FirstError);

        mockCityService.Verify(
            service => service.GetCityByIdAsync(cityId),
            Times.Once,
            "GetCityByIdAsync should be called once with the correct country ID.");
    }

}
