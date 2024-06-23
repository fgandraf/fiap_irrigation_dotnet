using Irrigation.Core.Models;
using Irrigation.Infra.Data;
using SecureIdentity.Password;

namespace Irrigation.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void InitiateEmptyDataBase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IrrigationDataContext>();

        #region CreateRoles
        if (!context.Roles.Any())
        {
            context.Roles.Add(new Role { Name = "admin" });
            context.Roles.Add(new Role { Name = "user" });
            context.SaveChanges();
        }
        #endregion
        
        #region CreateUserAdmin
        if (!context.Users.Any())
        {
            var admin = new User
            {
                Active = true,
                Email = "admin@admin",
                Name = "Administrador",
                PasswordHash = PasswordHasher.Hash("admin"),
                Roles =
                [
                    context.Roles.FirstOrDefault(x => x.Name == "admin")
                ]
            };
            
            context.Users.Add(admin);
            context.SaveChanges();
        }
        #endregion
    }
}