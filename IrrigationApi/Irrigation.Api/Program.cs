global using Irrigation.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurationKeys();
builder.AddDatabase();
builder.AddRepositoryServices();
builder.AddJwtAuthentication();
builder.AddAuthorizationPolicies();
builder.AddSwaggerConfigurations();
builder.JsonIgnoreCycles();
builder.AddVersionControls();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.InitiateEmptyDataBase();

app.Run();