using Irrigation.Api.Controllers;
using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace Irrigation.Tests;

public class ScheduleControllerTest
{
    private readonly ScheduleController _controller;
    
    public ScheduleControllerTest()
    {
        // Arrange
        var context = new Mock<IrrigationDataContext>();
        context.Setup(m => m.Schedules).ReturnsDbSet(GetSchedules());
        var repository = new ScheduleRepository(context.Object);
        _controller = new ScheduleController(repository);
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
    
    private List<Schedule> GetSchedules()
    {
        return
        [
            new Schedule { Id = 1, StartTime = new DateTime(2024, 06, 22, 10, 15, 00), EndTime = new DateTime(2024, 06, 22, 11, 25, 00) },
            new Schedule { Id = 2, StartTime = new DateTime(2024, 06, 23, 14, 35, 00), EndTime = new DateTime(2024, 06, 23, 15, 35, 00) },
            new Schedule { Id = 3, StartTime = new DateTime(2024, 06, 24, 20, 10, 00), EndTime = new DateTime(2024, 06, 24, 22, 20, 00) }
        ];
    }
}