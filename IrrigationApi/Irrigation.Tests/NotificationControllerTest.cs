using Irrigation.Api.Controllers;
using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace Irrigation.Tests;

public class NotificationControllerTest
{
    private readonly NotificationController _controller;
    
    public NotificationControllerTest()
    {
        // Arrange
        var context = new Mock<IrrigationDataContext>();
        context.Setup(m => m.Notifications).ReturnsDbSet(GetNotificartions());
        var repository = new NotificationRepository(context.Object);
        _controller = new NotificationController(repository);
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
    
    private static List<Notification> GetNotificartions()
    {
        var area = new Area { Id = 1, Description = "Area 1", Location = "Location 1", Size = "", Sensors = [] };
        var sensor = new Sensor { Id = 1, Type = $"Type 1", Location = $"Location 1", Area = area, Notifications = [], Weathers = [] };
        
        var datas = new List<Notification>();
        for (var i = 1; i < 4; i++)
            datas.Add(new Notification
            {
                Id = i,
                Description = $"Description {i}0",
                Timestamp = new DateTime(2024, 06, i, 1 + i, i * 2, 00),
                Sensor = sensor
            });
                    
        return datas;
    }
}