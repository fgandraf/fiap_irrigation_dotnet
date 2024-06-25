using Irrigation.Api.Controllers;
using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace Irrigation.Tests;

public class AreaControllerTest
{
    private readonly AreaController _controller;
    
    public AreaControllerTest()
    {
        // Arrange
        var context = new Mock<IrrigationDataContext>();
        context.Setup(m => m.Areas).ReturnsDbSet(GetAreas());
        var repository = new AreaRepository(context.Object);
        _controller = new AreaController(repository);
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
    
    private static List<Area> GetAreas()
    {
        var datas = new List<Area>();
        for (var i = 1; i < 4; i++)
            datas.Add(new Area
            {
                Id = i, 
                Description = $"Area {i}", 
                Location = $"Location {i}", 
                Size = $"{i}00", 
                Sensors = []
            });
        return datas;
    }
}