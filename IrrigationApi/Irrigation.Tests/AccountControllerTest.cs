using Irrigation.Api.Controllers;
using Irrigation.Api.Services;
using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using Irrigation.Infra.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Irrigation.Tests;

public class AccountControllerTest
{
    private readonly AccountController _controller;
    
    public AccountControllerTest()
    {
        // Arrange
        var context = new Mock<IrrigationDataContext>();
        context.Setup(m => m.Users).ReturnsDbSet(GetUsers());
        var repository = new UserRepository(context.Object);
        _controller = new AccountController(new TokenService(), repository);
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
    
    private static List<User> GetUsers()
    {
        var datas = new List<User>();
        for (var i = 1; i < 4; i++)
            datas.Add(new User
            {
                Id = i, 
                Name = $"User {i}", 
                Email = $"user{i}@email.com", 
                PasswordHash = PasswordHasher.Hash("password123"), 
                Active = true, 
                Roles = [new Role
                {
                    Id = 2, 
                    Name = "user"
                }]
            });
        return datas;
    }
}