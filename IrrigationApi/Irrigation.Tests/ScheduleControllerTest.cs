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
    
    private static List<Schedule> GetSchedules()
    {
        var datas = new List<Schedule>();
        for (var i = 1; i < 4; i++)
            datas.Add(new Schedule
            {
                Id = i, 
                StartTime = new DateTime(2024, 06, i, 1+i, i*2, 00), 
                EndTime = new DateTime(2024, 06, i, 1+i, i*2, 00)
            });
        return datas;
    }
}