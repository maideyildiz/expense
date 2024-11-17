using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.CityOperations.Queries;
using ExpenseTracker.Application.Common.Interfaces.Services;

using Moq;

namespace ExpenseTracker.Test.UnitTests.CityOperations.Queries;


public class GetCitiesQueryHandlerTests
{
    [Fact]
    public async Task HandleGetCitiesQuery_WhenCitiesExist_ShouldReturnCities()
    {
        // Arrange
        var countryId = Guid.NewGuid();
        var page = 1;
        var pageSize = 10;
        var totalCount = 25;
        var cities = new List<GetCityResult>
    {
        new GetCityResult (Guid.NewGuid(),  "City1" ),
        new GetCityResult (Guid.NewGuid(),  "City2" ),
        new GetCityResult (Guid.NewGuid(),  "City3" ),
    };

        var mockCityService = new Mock<ICityService>();
        var mockCountryService = new Mock<ICountryService>();
        mockCityService
            .Setup(service => service.GetCitiesByCountryIdAsync(countryId, page, pageSize))
            .ReturnsAsync((cities, totalCount));

        var handler = new GetCitiesQueryHandler(mockCityService.Object, mockCountryService.Object);
        var query = new GetCitiesQuery(countryId, page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError, "Result should not contain an error.");
        Assert.Equal(cities.Count, result.Value.Items.Count);
        Assert.Equal(totalCount, result.Value.TotalCount);
        Assert.Equal(page, result.Value.Page);
        Assert.Equal(pageSize, result.Value.PageSize);

        mockCityService.Verify(
            service => service.GetCitiesByCountryIdAsync(countryId, page, pageSize),
            Times.Once,
            "GetCitiesByCountryIdAsync should be called once with the correct parameters.");
    }

    [Fact]
    public async Task HandleGetCitiesQuery_WhenCitiesDoNotExist_ShouldReturnEmptyList()
    {
        // Arrange
        var countryId = Guid.NewGuid();
        var page = 1;
        var pageSize = 10;
        var totalCount = 0;
        var cities = new List<GetCityResult>();

        var mockCityService = new Mock<ICityService>();
        var mockCountryService = new Mock<ICountryService>();
        mockCityService
            .Setup(service => service.GetCitiesByCountryIdAsync(countryId, page, pageSize))
            .ReturnsAsync((cities, totalCount));

        var handler = new GetCitiesQueryHandler(mockCityService.Object, mockCountryService.Object);
        var query = new GetCitiesQuery(countryId, page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError, "Result should not contain an error.");
        Assert.Empty(result.Value.Items);
        Assert.Equal(totalCount, result.Value.TotalCount);
        Assert.Equal(page, result.Value.Page);
        Assert.Equal(pageSize, result.Value.PageSize);

        mockCityService.Verify(
            service => service.GetCitiesByCountryIdAsync(countryId, page, pageSize),
            Times.Once,
            "GetCitiesByCountryIdAsync should be called once with the correct parameters.");
    }
    [Fact]
    public async Task HandleGetCitiesQuery_WhenCountryDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var invalidCountryId = Guid.NewGuid(); // Geçersiz ülke ID'si
        var page = 1;
        var pageSize = 10;

        var mockCountryService = new Mock<ICountryService>();
        var mockCityService = new Mock<ICityService>();

        // Geçersiz ülke ID'si için hata döndür
        mockCountryService
            .Setup(service => service.GetCountryByIdAsync(invalidCountryId))
            .ReturnsAsync(Error.NotFound("CountryNotFound", "The specified country does not exist."));

        var handler = new GetCitiesQueryHandler(mockCityService.Object, mockCountryService.Object);

        var query = new GetCitiesQuery(invalidCountryId, page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError, "Result should contain an error.");
        Assert.Contains(result.Errors, error => error.Code == "CountryNotFound");
        Assert.Contains(result.Errors, error => error.Description == "The specified country does not exist.");

        mockCountryService.Verify(
            service => service.GetCountryByIdAsync(invalidCountryId),
            Times.Once,
            "GetCountryByIdAsync should be called once with the invalid country ID."
        );

        mockCityService.Verify(
            service => service.GetCitiesByCountryIdAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()),
            Times.Never,
            "GetCitiesByCountryIdAsync should not be called when the country does not exist."
        );
    }





}