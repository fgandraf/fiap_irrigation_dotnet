using Irrigation.Api.Controllers;
using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace Irrigation.Tests;

public class SensorControllerTest
{
    private readonly SensorController _controller;
    
    public SensorControllerTest()
    {
        // Arrange
        var context = new Mock<IrrigationDataContext>();
        context.Setup(m => m.Sensors).ReturnsDbSet(GetSensors());
        var repository = new SensorRepository(context.Object);
        _controller = new SensorController(repository);
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
    
    private static List<Sensor> GetSensors()
    {
        var datas = new List<Sensor>();
        for (var i = 1; i < 4; i++)
            datas.Add(new Sensor
            {
                Id = i, 
                Type = $"Type {i}0", 
                Location = $"Location {i}", 
                Area = new Area
                {
                    Id = 1, 
                    Description = "Area 1", 
                    Location = "Location 1", 
                    Size = "", 
                    Sensors = []
                }, 
                Notifications = [], 
                Weathers = []
            });
        return datas;
    }
}