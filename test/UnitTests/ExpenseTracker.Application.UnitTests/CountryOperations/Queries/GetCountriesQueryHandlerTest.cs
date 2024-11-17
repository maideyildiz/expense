using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Application.CountryOperations.Queries;

using Moq;

namespace ExpenseTracker.Application.UnitTests.CountryOperations.Queries;


public class GetCountriesQueryHandlerTests
{
    [Fact]
    public async Task HandleGetCountriesQuery_WhenCountriesExist_ShouldReturnCountries()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var totalCount = 15;
        var countries = new List<GetCountryResult>
    {
        new GetCountryResult ( Guid.NewGuid(), "Country1" ),
        new GetCountryResult ( Guid.NewGuid(), "Country2" ),
        new GetCountryResult ( Guid.NewGuid(), "Country3" ),
    };

        var mockCountryService = new Mock<ICountryService>();
        mockCountryService
            .Setup(service => service.GetCountriesAsync(page, pageSize))
            .ReturnsAsync((countries, totalCount));

        var handler = new GetCountriesQueryHandler(mockCountryService.Object);
        var query = new GetCountriesQuery(page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError == false, "Result should not contain an error.");
        Assert.Equal(countries.Count, result.Value.Items.Count);
        Assert.Equal(totalCount, result.Value.TotalCount);
        Assert.Equal(page, result.Value.Page);
        Assert.Equal(pageSize, result.Value.PageSize);

        mockCountryService.Verify(
            service => service.GetCountriesAsync(page, pageSize),
            Times.Once,
            "GetCountriesAsync should be called once with the correct page and page size.");
    }

    [Fact]
    public async Task HandleGetCountriesQuery_WhenCountriesDoNotExist_ShouldReturnEmptyList()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var totalCount = 0; // Ülke yok
        var countries = new List<GetCountryResult>();

        var mockCountryService = new Mock<ICountryService>();
        mockCountryService
            .Setup(service => service.GetCountriesAsync(page, pageSize))
            .ReturnsAsync((countries, totalCount));

        var handler = new GetCountriesQueryHandler(mockCountryService.Object);
        var query = new GetCountriesQuery(page, pageSize);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError == false, "Result should not contain an error.");
        Assert.Empty(result.Value.Items); // Liste boş olmalı
        Assert.Equal(totalCount, result.Value.TotalCount); // Toplam sayısı 0 olmalı
        Assert.Equal(page, result.Value.Page); // Sayfa numarası doğru olmalı
        Assert.Equal(pageSize, result.Value.PageSize); // Sayfa boyutu doğru olmalı

        mockCountryService.Verify(
            service => service.GetCountriesAsync(page, pageSize),
            Times.Once,
            "GetCountriesAsync should be called once with the correct page and page size.");
    }


}