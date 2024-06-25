using Irrigation.Api.Controllers;
using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace Irrigation.Tests;

public class WeatherControllerTest
{
    private readonly WeatherController _controller;
    
    public WeatherControllerTest()
    {
        // Arrange
        var context = new Mock<IrrigationDataContext>();
        context.Setup(m => m.Weathers).ReturnsDbSet(GetWeathers());
        var repository = new WeatherRepository(context.Object);
        _controller = new WeatherController(repository);
    }
    
    [Fact]
    public void GetAll_ReturnsHttpStatusCode200()
    {
        // Act
        var result = _controller.GetAll();
        
        // Assert
        var statusCode = result switch
        {
            ObjectResult objectResult => objectResult.StatusCode,
            StatusCodeResult statusCodeResult => statusCodeResult.StatusCode,
            _ => null
        };
        
        Assert.Equal(200, statusCode);
    }
    
    private static List<Weather> GetWeathers()
    {
        var area = new Area { Id = 1, Description = "Area 1", Location = "Location 1", Size = "", Sensors = [] };
        var sensor = new Sensor { Id = 1, Type = $"Type 1", Location = $"Location 1", Area = area, Notifications = [], Weathers = [] };
        
        var datas = new List<Weather>();
        for (var i = 1; i < 4; i++)
            datas.Add(new Weather
            {
                Id = i,
                Timestamp = new DateTime(2024, 06, i, 1 + i, i * 2, 00),
                Temperature = i+10,
                Humidity = i+50,
                Description = $"Description {i}0",
                Sensor = sensor
            });
                    
        return datas;
    }
}